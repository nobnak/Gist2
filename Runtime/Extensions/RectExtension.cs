using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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

        public static float4 CreateAsRect(float2 pos, float2 size) => new float4(pos, size);
        public static float2 SizeAsRect(this float4 r) => r.zw;
        public static float2 CenterAsRect(this float4 r) => new float2(r.x + r.z * 0.5f, r.y + r.w * 0.5f);
        public static float2 MinAsRect(this float4 r) => r.xy;
        public static float2 MaxAsRect(this float4 r) => r.xy + r.zw;
    }
}
