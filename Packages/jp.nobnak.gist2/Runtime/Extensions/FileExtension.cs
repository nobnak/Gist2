using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Gist2.Extensions.FileExt {

    public static class FileExtension {
        [Obsolete("Use SaveJsonTo instead.")]
        public static bool SaveTo(this object obj, string path) {
            return SaveJasonTo(obj, path);
        }
        [Obsolete("Use LoadJsonOverwriteFrom instead.")]
        public static bool LoadOverwriteFrom(this object obj, string path) {
            return LoadJsonOverwriteFrom(obj, path);
        }
        [Obsolete("Use LoadJsonFrom instead.")]
        public static T LoadFrom<T>(this string path) {
            return JsonUtility.FromJson<T>(path);
        }

        public static bool SaveJasonTo(this object obj, string path, bool prettyPrint = true) {
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
        public static T LoadJsonFrom<T>(this string path) {
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
