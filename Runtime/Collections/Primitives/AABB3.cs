using Gist2.Collections.Primitives.Extensions;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace Gist2.Collections.Primitives {

    [BurstCompile]
    public struct AABB3 {

        public readonly float3 Min;
        public readonly float3 Max;

        public AABB3(float3 min, float3 max) {
            this.Min = min;
            this.Max = max;
        }

        public readonly float3 Center {
            get { 
                this.Center(out var center); 
                return center; 
            }
        }
        public readonly float3 Size {
            get {
                this.Size(out var size);
                return size;
            }
        }

        public static AABB3 FromCenterAndSize(float3 center, float3 size) {
            var extent = size / 2;
            var min = center - extent;
            var max = min + size;
            return new AABB3(min, max);
        }
    }
}
