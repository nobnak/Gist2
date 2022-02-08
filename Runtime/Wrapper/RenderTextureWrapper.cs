using Gist2.Deferred;
using Gist2.Extensions.ComponentExt;
using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace Gist2.Wrapper {

    public class RenderTextureWrapper : IValue<RenderTexture>, IAssurance, System.IDisposable {

        protected Tuner tuner = new Tuner();

        protected Vector2Int size;
        protected RenderTexture tex;

        protected Assurance init;

        public RenderTextureWrapper() {
            init = new Assurance();
            init.Renew += () => {
                RenderTexture v = null;
                if (size.x >= 4 && size.y >= 4)
                    v = new RenderTexture(size.x, size.y, tuner.depth, tuner.gf);
                Value = v;
            };
        }

        #region interafce
        public event Action Changed;

		#region IAssurance
		public void Assure() => init.Assure();
		public void Expire() => init.Expire();
        #endregion

        #region IDisposable
        public void Dispose() {
           if (tex != null) {
                tex.Destroy();
                tex = null;
            }
        }
        #endregion

        public Tuner CurrTuner {
            get => tuner.DeepCopy();
            set {
                tuner = value.DeepCopy();
                init.Expire();
            }
        }
        public Vector2Int Size {
            get => size;
            set {
                if (size != value) {
                    size = value;
                    init.Expire();
                }
            }
        }
        public RenderTexture Value { 
            get {
                init.Assure();
                return tex;
            }
            protected set {
                var prev = tex;
                try {
                    tex = value;
                    Changed?.Invoke();
                } finally {
                    prev.Destroy();
                }
            }
        }
        #endregion

        #region static
        public static implicit operator RenderTexture(RenderTextureWrapper h) => h.Value;
        #endregion

        #region definitions
        [System.Serializable]
        public class Tuner {
            public RenderTextureFormat gf = RenderTextureFormat.ARGBHalf;
            public int depth = 0;
        }
        #endregion
    }
}
