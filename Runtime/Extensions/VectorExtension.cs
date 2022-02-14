using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Extensions.VectorExt {

    public static class VectorExtension {

        public static Vector2 Div(this float a, Vector2 b) => new Vector2(a / b.x, a / b.y);
        public static Vector2 Div(this float a, Vector2Int b) => new Vector2(a / b.x, a / b.y);

        public static float Cross(this Vector2 a, Vector2 b) {
            return a.x * b.y - a.y * b.x;
        }

		public static int Area(this Vector2Int v) => v.x * v.y;
    }
}
