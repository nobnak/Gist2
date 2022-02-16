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

		public static T Bilinear<T>(this IReadableTexture<T> tex, Vector2 uv) where T : struct {
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

			var v = t.Lerp(s.Lerp(vlb, vrb), s.Lerp(vlt, vrt));
			return v;
		}

		public static T Lerp<T>(this float t, T start, T end) {
			if (start is Vector4)
				return (T)(object)Vector4.Lerp((Vector4)(object)start, (Vector4)(object)end, t);

			if (start is Color32)
				return (T)(object)Color32.Lerp((Color32)(object)start, (Color32)(object)end, t);

			if (start is Color)
				return (T)(object)Color.Lerp((Color)(object)start, (Color)(object)end, t);

			if (start is float)
				return (T)(object)Mathf.Lerp((float)(object)start, (float)(object)end, t);

			throw new System.NotSupportedException($"Type not supported : {typeof(T).Name}");
		}
	}
}
