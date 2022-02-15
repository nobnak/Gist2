using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gist2.Extensions.MathExt {

	public static class MathExtension {

		public static float Clamp(this float v, float min, float max) => Mathf.Clamp(v, min, max);

		public static bool MakeQuadVerticesOnPlane(this IEnumerable<Vector2> v, out Vector4 z, float z0 = 1f) {
			var p = v.Take(4).ToArray();

			var hmat = Matrix4x4.identity;
			hmat[0] = p[1].x; hmat[4] = -p[2].x; hmat[8] = p[3].x;
			hmat[1] = p[1].y; hmat[5] = -p[2].y; hmat[9] = p[3].y;
			hmat[2] = 1f; hmat[6] = -1f; hmat[10] = 1f;
			var hinv = hmat.inverse;

			var det = hmat.determinant;
			var result = det <= -Mathf.Epsilon || Mathf.Epsilon < det;
			if (!result) {
				z = z0 * Vector4.one;
				return result;
			}

			var h0 = new Vector3(p[0].x, p[0].y, z0);
			var z1 = hinv.MultiplyPoint(h0);
			z = new Vector4(z0, z1.x, z1.y, z1.z);
			return result;
		}
	}
}
