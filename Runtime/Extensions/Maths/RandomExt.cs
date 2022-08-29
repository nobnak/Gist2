using Gist2.Collections.Primitives;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Mathematics;

namespace Gis2.Extensions.Maths.RandomExt {

    [BurstCompile]
    public static class RandomExt {

        public static Random rand = Random.CreateFromIndex(31);

        public static void Sample(this in AABB2 aabb, out float2 v)
             => v = rand.NextFloat2(aabb.Min, aabb.Max);
        public static void Sample(this in AABB3 aabb, out float3 v)
             => v = rand.NextFloat3(aabb.Min, aabb.Max);

        public static void Sample(this UnityEngine.Transform tr, out float3 v)
            => v = tr.TransformPoint(rand.NextFloat3() - 0.5f);
    }
}
