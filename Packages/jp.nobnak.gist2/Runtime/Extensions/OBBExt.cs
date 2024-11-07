using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Gist2.Extensions {

	public static class OBBExt {

		public enum Axis { X = 1, Y = 1 << 1, Z = 1 << 2 }

		public static int ContainsAxis(this Transform cube, float3 worldPos, float size_x = 1f, float size_y = 1f, float size_z = 1f) {

			var p = cube.InverseTransformPoint(worldPos);

			size_x *= 0.5f;
			size_y *= 0.5f;
			size_z *= 0.5f;

			var res = 0;
			if (-size_x <= p.x && p.x <= size_x) res |= (int)Axis.X;
			if (-size_y <= p.y && p.y <= size_y) res |= (int)Axis.Y;
			if (-size_z <= p.z && p.z <= size_z) res |= (int)Axis.Z;

#if UNITY_EDITOR
			//Debug.Log($"OOB.Contains: res={res}, local_pos={p}, size={new float3(size_x, size_y, size_z)}");
#endif

			return res;
		}
	}
}
