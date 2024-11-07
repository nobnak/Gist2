using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Gist2.Extensions.LODExt {

	public static class LODExtension {

		public static int LOD(this int size, int lod) {
			if (lod > 0) {
				size >>= lod;
			} else if (lod < 0) {
				size <<= -lod;
			}
			return size;
		}
		public static int2 LOD(int width, int height, int lod) => new int2(width.LOD(lod), height.LOD(lod));
		public static int2 LOD(this int2 size, int lod) => LOD(size.x, size.y, lod);
	}
}
