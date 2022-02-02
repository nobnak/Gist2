using Gist2.Extensions.ComponentExt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Resizable {

    public class RenderTextureResizable : System.IDisposable {

        protected Vector2Int size = new Vector2Int(4, 4);

        protected RenderTexture tex;
        protected System.Func<Vector2Int, RenderTexture> creator;

        public RenderTextureResizable(System.Func<Vector2Int, RenderTexture> creator) {
            this.creator = creator;
        }

        #region interface

        #region IDisposable
        public void Dispose() {
            Invalidate();
        }
        #endregion

        public Vector2Int Size {
            get => size;
            set {
                if (size != value) {
                    size = value;
                    Invalidate();
                }
            }
        }
        public RenderTexture Value {
            get {
                if (TexIsNull) tex = creator(size);
                return tex;
            }
        }


        public bool TexIsNull => tex == null;
        public void Invalidate() {
            if (!TexIsNull) tex.Destroy();
            tex = null;
        }
        #endregion
    }
}
