using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Extensions.ComponentExt {

    public static class ObjectExtension {

        public static T Destroy<T>(this T v) where T : UnityEngine.Object {
            if (v != null) {
                if (Application.isPlaying)
                    UnityEngine.Object.Destroy(v);
                else
                    UnityEngine.Object.DestroyImmediate(v);
            }
            return v;
        }
        public static T DeepCopy<T>(this T v) => JsonUtility.FromJson<T>(JsonUtility.ToJson(v));
    }
}