using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Gist2.Extensions.VectorExt {

    public static class VectorExtension {

        public static float2 Div(this float a, float2 b) => new float2(a / b.x, a / b.y);
        public static float2 Div(this float a, int2 b) => new float2(a / b.x, a / b.y);

        public static float Cross(this float2 a, float2 b) {
            return a.x * b.y - a.y * b.x;
        }

		public static int Area(this int2 v) => v.x * v.y;
    }
}
