using Gist2.Deferred;
using Gist2.Extensions.ComponentExt;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Gist2.Visualization {

	[ExecuteAlways]
	public class OutputMixer : MonoBehaviour {

		public static readonly int P_Mixer = Shader.PropertyToID("_Mixer");
		public static readonly int P_Gain = Shader.PropertyToID("_Gain");

		public static readonly int[] P_Ts = new int[] {
			Shader.PropertyToID("_T0"),
			Shader.PropertyToID("_T1"),
			Shader.PropertyToID("_T2"),
			Shader.PropertyToID("_T3"),
		};

		public RuntimeData runtime = new RuntimeData();
		public Link link = new Link();
		public Tuner tuner = new Tuner();

		protected Validator validator = new Validator();

		#region unity
		private void OnEnable() {
			validator.Reset();
			validator.OnValidate += () => {
				var dtex = runtime.directTextures;
				if (dtex.Count != P_Ts.Length) {
					while (dtex.Count < P_Ts.Length) dtex.Add(null);
					while (dtex.Count > P_Ts.Length) dtex.RemoveAt(dtex.Count - 1);
				}
			};
		}
		private void OnValidate() {
			validator.Invalidate();
		}
		private void Update() {
			validator.Validate();

			var m = link.output;
			if (m != null) {
				for (var i = 0; i < P_Ts.Length; i++) {
					Texture tex = null;
					if (i < link.inputs.Length)
						tex = link.inputs[i];
					if (tex == null && i < runtime.directTextures.Count)
						tex = runtime.directTextures[i];
					m.SetTexture(P_Ts[i], tex);
				}

				m.SetTexture(P_Mixer, link.mixer);

				m.SetVector(P_Gain, tuner.gain);
			}
		}
		#endregion

		#region methods
		public Tuner CurrTuner {
			get => tuner.DeepCopy();
			set {
				validator.Invalidate();
				tuner = value.DeepCopy();
			}
		}
		public void SetTexture(int channel, Texture tex) {
			validator.Validate();
			if (0 < channel && channel < P_Ts.Length)
				runtime.directTextures[channel] = tex;
		}
		#endregion

		[System.Serializable]
		public class RuntimeData {
			public List<Texture> directTextures = new List<Texture>();
		}
		[System.Serializable]
		public class Link {
			public Material output;

			public TextureHolder[] inputs;
			public TextureHolder mixer;
		}
		[System.Serializable]
		public class Tuner {
			[Tooltip("Mixer gain: x=Illusion, y=Noise")]
			public float4 gain = new float4(1f, 1f, 1f, 1f);
		}
	}
}
