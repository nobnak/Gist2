using Gist2.Extensions.EditorExt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Extensions.ScreenExt {

	public static class ScreenExtension {

		public static Vector2 UV(this Vector2Int screen, Vector2 pixelPos)
			=> new Vector2((float)pixelPos.x / screen.x, (float)pixelPos.y / screen.y);
		public static Vector2 UV(this Camera cam, Vector2 pixelPos) => cam.Size().UV(pixelPos);
		public static Vector2 UV(this Vector2 mousePosition) => Size().UV(mousePosition);
		public static Vector2 UV(this Vector3 mousePosition) => ((Vector2)mousePosition).UV();

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

		public static Vector2Int Size() => new Vector2Int(Screen.width, Screen.height);
		public static Vector2Int Size(this Texture tex) => new Vector2Int(tex.width, tex.height);
		public static Vector2Int Size(this Camera c) => new Vector2Int(c.pixelWidth, c.pixelHeight);
		public static Vector2Int Size(this Resolution r) => new Vector2Int(r.width, r.height);
		public static Vector2Int Size(this RenderTextureDescriptor desc) => new Vector2Int(desc.width, desc.height);

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
