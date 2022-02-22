using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Extensions.GeometryExt {

	public static class GeometryExtension {

		public static readonly (float dist, float t) DEFAULT_DISTANCE_RESULT = (float.PositiveInfinity, -1);

		public static (float dist, float t) Distance(this IEnumerable<Vector2> lineStrip, Vector2 v, Vector2 unit) {
			(float dist, float t) res = DEFAULT_DISTANCE_RESULT;

			var iter = lineStrip.GetEnumerator();
			if (!iter.MoveNext()) return res;

			var w = v * unit;

			var i = 0;
			var seg0 = iter.Current * unit;
			while (iter.MoveNext()) {
				var seg1 = iter.Current * unit;

				var lineseg = seg1 - seg0;
				var len = lineseg.magnitude;
				var segtan = lineseg / len;
				var wcos = Vector2.Dot(segtan, w - seg0);

				var closestw = (wcos < 0 ? seg0 : (wcos > len ? seg1 : seg0 + wcos * segtan));
				var dist = (w - closestw).magnitude;
				if (dist < res.dist) {
					res = (dist, i + wcos / len);
				}
				
				i++;
				seg0 = seg1;
			}

			return res;
		}
	}
}
