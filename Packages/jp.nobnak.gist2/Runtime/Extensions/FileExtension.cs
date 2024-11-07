using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Gist2.Extensions.FileExt {

    public static class FileExtension {

        public static bool SaveTo(this object obj, string path) {
            try {
                var json = JsonUtility.ToJson(obj);
                File.WriteAllText(path, json);
                return true;
            } catch (System.Exception e) {
                Debug.LogWarning(e);
                return false;
            }
        }

        public static bool LoadOverwriteFrom(this object obj, string path) {
            try {
                var json = File.ReadAllText(path);
                JsonUtility.FromJsonOverwrite(json, obj);
                return true;
            } catch(System.Exception e) {
                Debug.LogWarning(e);
                return false;
            }
        }
        public static T LoadFrom<T>(this string path) {
            try {
                var json = File.ReadAllText(path);
                return JsonUtility.FromJson<T>(json);
            } catch (System.Exception e) {
                Debug.LogWarning(e);
                return default;
            }
        }
    }
}
