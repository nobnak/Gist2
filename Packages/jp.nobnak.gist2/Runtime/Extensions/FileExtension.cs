using System;
using System.IO;
using UnityEngine;

namespace Gist2.Extensions.FileExt {

    public static class FileExtension {
        [Obsolete("Use SaveJsonTo instead.")]
        public static bool SaveTo(this object obj, string path) {
            return SaveJsonTo(obj, path);
        }
        [Obsolete("Use LoadJsonOverwriteFrom instead.")]
        public static bool LoadOverwriteFrom(this object obj, string path) {
            return LoadJsonOverwriteFrom(obj, path);
        }
        [Obsolete("Use LoadJsonFrom instead.")]
        public static T LoadFrom<T>(this string path) {
            return JsonUtility.FromJson<T>(path);
        }

        public static bool SaveJsonTo(this object obj, string path, bool prettyPrint = true) {
            try {
                var json = JsonUtility.ToJson(obj, prettyPrint);
                File.WriteAllText(path, json);
                return true;
            } catch (System.Exception e) {
                Debug.LogWarning(e);
                return false;
            }
        }
        public static bool LoadJsonOverwriteFrom(this object obj, string path) {
            try {
                var json = File.ReadAllText(path);
                JsonUtility.FromJsonOverwrite(json, obj);
                return true;
            } catch(System.Exception e) {
                Debug.LogWarning(e);
                return false;
            }
        }
        public static bool LoadJsonFrom<T>(this string path, out T result) {
            try {
                var json = File.ReadAllText(path);
                result = JsonUtility.FromJson<T>(json);
                return true;
            } catch (System.Exception e) {
                Debug.LogWarning(e);
                result = default;
                return false;
            }
        }
    }
}
