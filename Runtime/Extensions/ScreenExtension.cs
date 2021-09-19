using Gist2.Extensions.EditorExt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Extensions.ScreenExt {

	public static class ScreenExtension {

        public static Vector2 UV(this Vector3 mousePosition) {
            var uv = new Vector2(
                (float)mousePosition.x / Screen.width, 
                (float)mousePosition.y / Screen.height);
            return uv;
        }
		public static int LOD(this int size, int lod) {
			if (lod > 0) {
				size >>= lod;
			} else if (lod < 0) {
				size <<= -lod;
			}
			return size;
		}
		public static Vector2Int LOD(int width, int height, int lod) => new Vector2Int(width.LOD(lod), height.LOD(lod));
		public static Vector2Int LOD(this Vector2Int size, int lod)	=> LOD(size.x, size.y, lod);

		public static Vector2Int Size(this Texture tex) => new Vector2Int(tex.width, tex.height);
		public static Vector2Int Size(this Camera c) => new Vector2Int(c.pixelWidth, c.pixelHeight);
		public static Vector2Int Size(this Resolution r) => new Vector2Int(r.width, r.height);

		public static float Aspect(this Vector2Int v) => v.x / (float)v.y;

		public static float DpiScale(this float baseScale, float targetDpi = 96f) {
			var scale = Screen.dpi / targetDpi;
			if (EditorExtension.LowResolutionAspectRatio)
				scale *= 0.5f;
			return baseScale * scale;
		}
		public static void DpiScale(float targetDpi = 96f) {
			var scale = 1f.DpiScale(targetDpi);
			GUIUtility.ScaleAroundPivot(new Vector2(scale, scale), Vector2.zero);
		}
	}
}
