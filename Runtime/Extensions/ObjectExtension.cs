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

        public static T Clone<T>(this T t) {
            var s = (T)System.Activator.CreateInstance(typeof(T));
            foreach (var f in typeof(T).GetFields())
                f.SetValue(s, f.GetValue(t));
            foreach (var p in t.GetType().GetProperties())
                p.SetValue(s, p.GetValue(t));
            return s;
        }
    }
}
