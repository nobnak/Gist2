using Gist2.Collections.Primitives.Extensions;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace Gist2.Collections.Primitives {

    [BurstCompile]
    public struct AABB2 {

        public readonly float2 Min;
        public readonly float2 Max;

        public AABB2(float2 min, float2 max) {
            this.Min = min;
            this.Max = max;
        }

        public readonly float2 Center {
            get { 
                this.Center(out var center); 
                return center; 
            }
        }
        public readonly float2 Size {
            get {
                this.Size(out var size);
                return size;
            }
        }

        public static AABB2 FromCenterAndSize(float2 center, float2 size) {
            var extent = size / 2;
            var min = center - extent;
            var max = min + size;
            return new AABB2(min, max);
        }
    }
}
