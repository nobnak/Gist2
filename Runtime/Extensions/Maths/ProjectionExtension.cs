using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace Gist2.Extensions.Maths {

	public static class ProjectionExtension {

        public static bool ProjectTrapIntoQuad(this Span<float> xy, Span<float> z4, float z0 = 1f) {
            var hmat = new float4x4(
                xy[2], -xy[4], xy[6], 0,
                xy[3], -xy[5], xy[7], 0,
                1f, -1f, 1f, 0,
                0, 0, 0, 1);
            var hinv = math.inverse(hmat);

            var det = math.determinant(hmat);
            var result = det <= -math.EPSILON || math.EPSILON < det;
            if (!result) {
                z4[0] = z4[1] = z4[2] = z4[3] = z0;
                return result;
            }

            var h0 = new float4(xy[0], xy[1], z0, 1);
            var z1 = math.mul(hinv, h0);
            z4[0] = z0; z4[1] = z1.x; z4[2] = z1.y; z4[3] = z1.z;
            return result;
        }
	}
}
