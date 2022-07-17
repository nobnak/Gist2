using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace Gist2.Extensions.Maths {

	public static class ProjectionExtension {

		public static bool MakeQuadVerticesOnPlane(this IEnumerable<float2> v, out float4 z, float z0 = 1f) {
			var p = v.Take(4).ToArray();

			var hmat = float4x4.identity;
			hmat[0] = p[1].x; hmat[4] = -p[2].x; hmat[8] = p[3].x;
			hmat[1] = p[1].y; hmat[5] = -p[2].y; hmat[9] = p[3].y;
			hmat[2] = 1f; hmat[6] = -1f; hmat[10] = 1f;
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
