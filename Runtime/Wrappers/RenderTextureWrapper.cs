using Gist2.Deferred;
using Gist2.Extensions.ComponentExt;
using Gist2.Interfaces;
using System;
using UnityEngine;

namespace Gist2.Wrappers {

	public class RenderTextureWrapper : IValue<RenderTexture>, IValidator, System.IDisposable {

        #region intializer
        public System.Func<Vector2Int, RenderTexture> Generator { get; set; }
        public event System.Action<RenderTextureWrapper> Changed;
        #endregion

        protected Vector2Int size;
        protected RenderTexture tex, prev;

        protected Validator defferedTexGen;

        public RenderTextureWrapper(System.Func<Vector2Int, RenderTexture> generator) {
            this.Generator = generator;

            defferedTexGen = new Validator();
            defferedTexGen.OnValidate += () => {
                SetTexture(Generator(size));
            };
            defferedTexGen.AfterValidate += () => {
                Notify();
                prev.Destroy();
            };
        }
        public RenderTextureWrapper() : this(null) { }

        #region interafce

		#region IAssurance
		public void Validate(bool force = false) => defferedTexGen.Validate(force);
		public void Invalidate() => defferedTexGen.Invalidate();
        #endregion

        #region IDisposable
        public void Dispose() {
            SetTexture(null);
            Notify();
            prev.Destroy();
        }
        #endregion

        public Vector2Int Size {
            get => size;
            set {
                if (size != value) {
                    size = value;
                    defferedTexGen.Invalidate();
                }
            }
        }
        public RenderTexture Value { 
            get {
                defferedTexGen.Validate();
				return tex;
            }
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
        public static implicit operator RenderTexture(RenderTextureWrapper h) => h.Value;
        #endregion
    }
}
