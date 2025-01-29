using Gist2.Deferred;
using Gist2.Extensions.ComponentExt;
using Gist2.Interfaces;
using System;
using Unity.Mathematics;
using UnityEngine;

namespace Gist2.Wrappers {

	public class RenderTextureWrapper : IValue<RenderTexture>, IValidator, System.IDisposable {

        #region intializer
        public System.Func<int2, RenderTexture> Generator { get; set; }
        public event System.Action<RenderTextureWrapper> Changed;
		#endregion

		protected int2 size = -1;
        protected RenderTexture tex, prev;

        protected Validator regenerate;

        public RenderTextureWrapper(System.Func<int2, RenderTexture> generator) {
            this.Generator = generator;

            regenerate = new Validator();
            regenerate.OnValidate += () => {
                SetTexture((size.x > 0 && size.y > 0) ? Generator(size) : null);
            };
            regenerate.AfterValidate += () => {
                Notify();
                prev.Destroy();
            };
        }
        public RenderTextureWrapper() : this(null) { }

        #region interafce

		#region IAssurance
		public void Validate(bool force = false) => regenerate.Validate(force);
		public void Invalidate() => regenerate.Invalidate();
        #endregion

        #region IDisposable
        public void Dispose() {
			size = -1;
            SetTexture(null);
            Notify();
            prev.Destroy();
        }
		#endregion

		#region properties
		public int2 Size {
            get => size;
            set {
                if (!size.Equals(value)) {
                    size = value;
                    regenerate.Invalidate();
                }
            }
        }
        public RenderTexture Value { 
            get {
                regenerate.Validate();
				return tex;
            }
        }
		#endregion

		public void Release() {
			regenerate.Invalidate();
			SetTexture(null);
			Notify();
			prev.Destroy();
		}
		#endregion

		#region member
		protected void SetTexture(RenderTexture next) {
            prev = tex;
            tex = next;
        }
        protected void Notify() => Changed?.Invoke(this);
        #endregion

        #region static
        public static implicit operator RenderTexture(RenderTextureWrapper h) 
            => (h != null) ? h.Value : null;
        #endregion
    }
}
