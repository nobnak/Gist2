using System.Collections;
using System.Collections.Generic;
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
		public static Vector2Int LOD(int width, int height, int lod) => new Vector2Int(width.LOD(lod), height.LOD(lod));
		public static Vector2Int LOD(this Vector2Int size, int lod) => LOD(size.x, size.y, lod);
	}
}
