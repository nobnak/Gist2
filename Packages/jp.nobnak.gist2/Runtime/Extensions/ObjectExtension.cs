using UnityEngine;

namespace Gist2.Extensions.ComponentExt {

    public static class ObjectExtension {

        public static T Destroy<T>(this T v) where T : Object {
            if (v != null) {
                if (Application.isPlaying)
                    Object.Destroy(v);
                else
                    Object.DestroyImmediate(v);
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

        public static bool EqualsAsJson<T>(this T a, T b) {
            if (a == null || b == null) return Object.Equals(a, b);
            var aJson = JsonUtility.ToJson(a);
            var bJson = JsonUtility.ToJson(b);
            return aJson == bJson;
        }
    }
}
