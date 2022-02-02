using Gist2.Extensions.EditorExt;
using Gist2.Extensions.SizeExt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Extensions.ScreenExt {

	public static class ScreenExtension {

		public static Vector2 UV(this Vector2Int screen, Vector2 pixelPos)
			=> new Vector2((float)pixelPos.x / screen.x, (float)pixelPos.y / screen.y);
		public static Vector2 UV(this Camera cam, Vector2 pixelPos) => cam.Size().UV(pixelPos);
		public static Vector2 UV(this Vector2 mousePosition) => ScreenSize().UV(mousePosition);
		public static Vector2 UV(this Vector3 mousePosition) => ((Vector2)mousePosition).UV();

		public static Vector2Int ScreenSize() => new Vector2Int(Screen.width, Screen.height);
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
