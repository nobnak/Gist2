using Gist2.Extensions.ComponentExt;
using Gist2.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Wrappers {

    public class CameraWrapper : System.IDisposable, IValue<Camera> {

        #region initializer
        public System.Func<Camera, Camera> Updator;
        #endregion

        protected Camera cam;

        public CameraWrapper(System.Func<Camera, Camera> updator) {
            this.Updator = updator;
        }
        public CameraWrapper() : this(null) { }

        #region interface

        #region IDisposable
        public void Dispose() {
            cam?.gameObject.Destroy();
        }
        #endregion

        #region IValue
        public Camera Value {
            get {
                Update();
                return cam;
            }
        }
        #endregion

        public void Update() {
            if (Updator != null)
                cam = Updator(cam);
        }
        #endregion

        #region static
        public static implicit operator Camera (CameraWrapper w) => w.Value;
        #endregion
    }
}
