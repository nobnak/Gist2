using Gist2.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Extensions.ReadableTextureExt {

	public static class ReadableTextureExtension {

		public static T Point<T>(this IReadableTexture<T> tex, Vector2 uv) where T : struct {
			var x = Mathf.FloorToInt(tex.Width * uv.x);
			var y = Mathf.FloorToInt(tex.Height * uv.y);

			x = Mathf.Clamp(x, 0, tex.Width -1);
			y = Mathf.Clamp(y, 0, tex.Height - 1);

			return tex[x, y];
		}

		public static Vector4 Bilinear(this IReadableTexture<Vector4> tex, Vector2 uv) {
			var x = tex.Width * uv.x;
			var y = tex.Height * uv.y;

			var xl = Mathf.Clamp(Mathf.FloorToInt(x), 0, tex.Width - 1);
			var yb = Mathf.Clamp(Mathf.FloorToInt(y), 0, tex.Height - 1);
			var xr = Mathf.Clamp(xl + 1, 0, tex.Width - 1);
			var yt = Mathf.Clamp(yb + 1, 0, tex.Height - 1);

			var s = Mathf.Clamp01(x - xl);
			var t = Mathf.Clamp01(y - yb);

			var vlb = tex[xl, yb];
			var vrb = tex[xr, yb];
			var vlt = tex[xl, yt];
			var vrt = tex[xr, yt];

			var v = Vector4.Lerp(Vector4.Lerp(vlb, vrb, s), Vector4.Lerp(vlt, vrt, s), t);
			return v;
		}
	}
}
