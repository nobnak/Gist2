using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace Gist2.Extensions.Maths {

	public static class ProjectionExtension {

		public static bool MakeQuadVerticesOnPlane(this IEnumerable<float2> v, out float4 z, float z0 = 1f) {
			var p = v.Take(4).ToArray();

            var hmat = new float4x4(
                p[1].x, -p[2].x, p[3].x, 0,
                p[1].y, -p[2].y, p[3].y, 0,
                1f, -1f, 1f, 0,
                0, 0, 0, 1);
			var hinv = math.inverse(hmat);

			var det = math.determinant(hmat);
			var result = det <= -math.EPSILON || math.EPSILON < det;
			if (!result) {
				z = z0 * new float4(1);
				return result;
			}

			var h0 = new float4(p[0].x, p[0].y, z0, 1);
			var z1 = math.mul(hinv, h0);
			z = new float4(z0, z1.x, z1.y, z1.z);
			return result;
		}
		public static bool MakeQuadVerticesOnPlane(this IEnumerable<Vector2> v, out Vector4 z, float z0 = 1f) { 
			var res = v.Select(v => (float2)v).MakeQuadVerticesOnPlane(out var y, z0);
			z = y;
			return res;
		}
	}
}
