using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Gist2.Scope {

	public class ScopedRenderTexture : System.IDisposable {

		protected RenderTexture target;
		protected RenderTexture prev;

		public ScopedRenderTexture(RenderTexture target) {
			this.target = target;
			this.prev = RenderTexture.active;
			RenderTexture.active = target;
		}

		public static implicit operator RenderTexture(ScopedRenderTexture value) {
			return value.target;
		}

		public void Dispose() {
			RenderTexture.active = prev;
		}
	}
}
