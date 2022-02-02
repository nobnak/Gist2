using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Extensions.SizeExt {

	public static class SizeExtension {

		public static Vector2Int Size(this Texture tex) => new Vector2Int(tex.width, tex.height);
		public static Vector2Int Size(this Camera c) => new Vector2Int(c.pixelWidth, c.pixelHeight);
		public static Vector2Int Size(this Resolution r) => new Vector2Int(r.width, r.height);
		public static Vector2Int Size(this RenderTextureDescriptor desc) => new Vector2Int(desc.width, desc.height);

		public static float Aspect(this Vector2Int v) => v.x / (float)v.y;

	}
}
