using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Extensions.RectExt {

    public static class RectExtension {

        public static Vector2 TransformPosition(this Rect rect, Vector2 pos)
            => new Vector2(rect.width * pos.x + rect.x, rect.height * pos.y + rect.y);
        
        public static Vector2 TransformVector(this Rect rect, Vector2 pos)
            => new Vector2(rect.width * pos.x, rect.height * pos.y);

        public static Vector2 InverseTransformPosition(this Rect rect, Vector2 pos)
            => new Vector2((pos.x - rect.x) / rect.width, (pos.y - rect.y) / rect.height);

        public static Vector2 InverseTransformVector(this Rect rect, Vector2 pos)
            => new Vector2(pos.x / rect.width, pos.y / rect.height);
    }
}
