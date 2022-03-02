using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Math.Primitives {

    public struct TRSMatrix {

        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;

        #region interface

        #region static
        public static TRSMatrix Identity = new TRSMatrix() { rotation = Quaternion.identity, scale = Vector3.one };

        public static implicit operator Matrix4x4 (TRSMatrix trs) => Matrix4x4.TRS(trs.position, trs.rotation, trs.scale);
        public static implicit operator TRSMatrix(Transform t)
            => new TRSMatrix() { position = t.localPosition, rotation = t.localRotation, scale = t.localScale };

        public static TRSMatrix Lerp(TRSMatrix a, TRSMatrix b, float t)
            => new TRSMatrix() {
                position = Vector3.Lerp(a.position, b.position, t),
                rotation = Quaternion.Lerp(a.rotation, b.rotation, t),
                scale = Vector3.Lerp(a.scale, b.scale, t)
            };
        #endregion

        #region Object
        public override string ToString()
            => $"<{GetType().Name} : pos={position}, rot={rotation}, scl={scale}>\n{(Matrix4x4)this}";
        #endregion

        #endregion
    }
}
