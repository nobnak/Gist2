using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace Gist2.Extensions.Maths {

    [BurstCompile]
    public static class ClampExtension {

        [BurstCompile]
        public static float Clamp01(this float v) => math.clamp(v, 0f, 1f);
        [BurstCompile]
        public static void Clamp01(this ref float2 v) => v = math.clamp(v, 0f, 1f);
        [BurstCompile]
        public static void Clamp01(this ref float3 v) => v = math.clamp(v, 0f, 1f);
        [BurstCompile]
        public static void Clamp01(this ref float4 v) => v = math.clamp(v, 0f, 1f);
    }
}
