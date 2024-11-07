using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace Gist2.Collections.Primitives.Extensions {

    [BurstCompile]
    public static class AABBExt {

        [BurstCompile]
        public static void Center(this in AABB2 aabb, out float2 center) => center = (aabb.Min + aabb.Max) / 2;
        [BurstCompile]
        public static void Center(this in AABB3 aabb, out float3 center) => center = (aabb.Min + aabb.Max) / 2;

        [BurstCompile]
        public static void Size(this in AABB2 aabb, out float2 size) => size = (aabb.Max - aabb.Min);
        [BurstCompile]
        public static void Size(this in AABB3 aabb, out float3 size) => size = (aabb.Max - aabb.Min);

        [BurstCompile]
        public static void Extent(this in AABB2 aabb, out float2 extent) {
            aabb.Size(out var size);
            extent = 0.5f * size;
        }
        [BurstCompile]
        public static void Extent(this in AABB3 aabb, out float3 extent) {
            aabb.Size(out var size);
            extent = 0.5f * size;
        }
    }
}
