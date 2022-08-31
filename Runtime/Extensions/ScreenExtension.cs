using Gist2.Extensions.EditorExt;
using Gist2.Extensions.SizeExt;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Gist2.Extensions.ScreenExt {

	public static class ScreenExtension {

		public static float2 UV(this int2 screen, float2 pixelPos)
			=> new float2((float)pixelPos.x / screen.x, (float)pixelPos.y / screen.y);
		public static float2 UV(this Camera cam, float2 pixelPos) => cam.Size().UV(pixelPos);
		public static float2 UV(this float2 mousePosition) => ScreenSize().UV(mousePosition);
		public static float2 UV(this float3 mousePosition) => mousePosition.xy.UV();

		public static int2 ScreenSize() => new int2(Screen.width, Screen.height);
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

        public static float3 IsometricUVToWorld(this Camera cmain, float x, float y, float z = 0f) {
            float aspect = cmain.aspect;
            var pos = cmain.ViewportToWorldPoint(0.5f * new Vector3(x / aspect, y));
            pos.z = z;
            return pos;
        }
    }
}
