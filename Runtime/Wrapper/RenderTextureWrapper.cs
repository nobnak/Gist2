using Gist2.Deferred;
using Gist2.Extensions.ComponentExt;
using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace Gist2.Wrapper {

    public class RenderTextureWrapper : IValue<RenderTexture>, IInitializable, System.IDisposable {

        protected Tuner tuner = new Tuner();

        protected Vector2Int size;
        protected RenderTexture tex;

        protected Init init;

        public RenderTextureWrapper() {
            init = new Init();
            init.Initialization += () => {
                RenderTexture v = null;
                if (size.x >= 4 && size.y >= 4)
                    v = new RenderTexture(size.x, size.y, tuner.depth, tuner.gf);
                Value = v;
            };
        }

        #region interafce
        public event Action Changed;

        #region IInitializable
        public void Initialize() => init.Initialize();
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
                init.Reset();
            }
        }
        public Vector2Int Size {
            get => size;
            set {
                if (size != value) {
                    size = value;
                    init.Reset();
                }
            }
        }
        public RenderTexture Value { 
            get {
                init.Initialize();
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
