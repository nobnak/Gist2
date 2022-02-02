using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Gist2.Scope {

	public class ScopedComputeBuffer<T> : System.IDisposable where T : struct {

		protected ComputeBuffer buf;

		public ScopedComputeBuffer(List<T> list) {
			buf = new ComputeBuffer(list.Count, Marshal.SizeOf<T>());
			buf.SetData(list);
		}

		public static implicit operator ComputeBuffer(ScopedComputeBuffer<T> sbuff) {
			return sbuff.buf;
		}

		public void Dispose() {
			buf.Dispose();
		}
	}
}
