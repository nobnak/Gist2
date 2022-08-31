using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Gist2.Extensions.SizeExt {

	public static class SizeExtension {

		public static int2 Size(this Texture tex) => new int2(tex.width, tex.height);
		public static int2 Size(this Camera c) => new int2(c.pixelWidth, c.pixelHeight);
		public static int2 Size(this Resolution r) => new int2(r.width, r.height);
		public static int2 Size(this RenderTextureDescriptor desc) => new int2(desc.width, desc.height);

		public static float Aspect(this int2 v) => v.x / (float)v.y;

		public static int2 ScreenWindow() => new int2(Screen.width, Screen.height);
	}
}
