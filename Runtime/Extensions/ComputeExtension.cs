using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace Gist2.Extensions {

	public static class ComputeExtension {

		public static int DispatchSize(this int threadCount, int groupSize) {
			return (threadCount - 1) / groupSize + 1;
		}
		public static Vector3Int DispatchSize(this ComputeShader cs, int kernel, Vector3Int counts) {
			uint x, y, z;
			cs.GetKernelThreadGroupSizes(kernel, out x, out y, out z);
			return new Vector3Int(
				DispatchSize(counts.x, (int)x),
				DispatchSize(counts.y, (int)y),
				DispatchSize(counts.z, (int)z));
		}
		public static Vector3Int DispatchSize(this ComputeShader cs, int kernel, int x, int y = 1, int z = 1) {
			return cs.DispatchSize(kernel, new Vector3Int(x, y, z));
		}
		public static Vector3Int CeilSize(this ComputeShader cs, int kernel, int x, int y = 1, int z = 1) {
			uint gsx, gsy, gsz;
			cs.GetKernelThreadGroupSizes(kernel, out gsx, out gsy, out gsz);
			return new Vector3Int(
				(int)gsx * DispatchSize(x, (int)gsx),
				(int)gsy * DispatchSize(y, (int)gsy),
				(int)gsz * DispatchSize(z, (int)gsz));
		}

		public static bool IsSupportedForReadPixels(this GraphicsFormat format) {
			return SystemInfo.IsFormatSupported(format, FormatUsage.ReadPixels);
		}

		public static void DispatchThread(this ComputeShader cs, int kernel, params int[] totalThreads) {
			var vThreads = Vector3Int.one;
			var i = 0;
			foreach (var t in totalThreads) vThreads[i++] = t;

			var disp = cs.DispatchSize(kernel, vThreads);
			cs.Dispatch(kernel, disp.x, disp.y, disp.z);
		}
	}
}
