using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Gist2.Extensions.GeometryExt {

	public static class GeometryExtension {

		public static readonly (float dist, float t) DEFAULT_DISTANCE_RESULT = (float.PositiveInfinity, -1);

		public static (float dist, float t) DistanceFromLines(this IEnumerable<float2> lineStrip, float2 v, float2 unit) {
			(float dist, float t) res = DEFAULT_DISTANCE_RESULT;

			var iter = lineStrip.GetEnumerator();
			if (!iter.MoveNext()) return res;

			var w = v * unit;

			var i = 0;
			var seg0 = iter.Current * unit;
			while (iter.MoveNext()) {
				var seg1 = iter.Current * unit;

				var lineseg = seg1 - seg0;
				var len = math.length(lineseg);
				var segtan = lineseg / len;
				var wcos = Vector2.Dot(segtan, w - seg0);

				var closestw = (wcos < 0 ? seg0 : (wcos > len ? seg1 : seg0 + wcos * segtan));
				var dist = math.length(w - closestw);
				if (dist < res.dist) {
					res = (dist, i + wcos / len);
				}
				
				i++;
				seg0 = seg1;
			}

			return res;
		}
		public static (float dist, float t) DistanceFromLines(this IEnumerable<float2> lineStrip, float2 v) {
			(float dist, float t) res = DEFAULT_DISTANCE_RESULT;

			var iter = lineStrip.GetEnumerator();
			if (!iter.MoveNext()) return res;
			var seg0 = iter.Current;

			var i = 0;
			while (iter.MoveNext()) {
				var seg1 = iter.Current;

				var lineseg = seg1 - seg0;
				var len = math.length(lineseg);
				var segtan = lineseg / len;
				var vcos = Vector2.Dot(segtan, v - seg0);

				var closestw = (vcos < 0 ? seg0 : (vcos > len ? seg1 : seg0 + vcos * segtan));
				var dist = math.length((float2)(v - closestw));
				if (dist < res.dist) {
					res = (dist, i + vcos / len);
				}

				i++;
				seg0 = seg1;
			}

			return res;
		}

		public static bool PointInPolygon(this IEnumerable<float2> boundary, float2 p) {
			var iter = boundary.GetEnumerator();

			if (!iter.MoveNext()) return false;
			var v0 = iter.Current;

			var n = 0;
			while (iter.MoveNext()) {
				var v1 = iter.Current;

				var inbetween = (v0.y <= p.y && p.y < v1.y) || (v1.y <= p.y && p.y < v0.y);
				if (inbetween) {
					var t = math.unlerp(v0.y, v1.y, p.y);
					var vt = math.lerp(v0.x, v1.x, t);
					if (p.x < vt) n++;
				}

				v0 = v1;
			}

			return (n % 2) != 0;
		}
	}
}
