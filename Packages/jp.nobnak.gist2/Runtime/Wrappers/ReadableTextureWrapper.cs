using Gist2.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Wrappers {

	public class ReadableTextureWrapper<T> : IReadableTexture<T> where T : struct {

		public event System.Action<ReadableTextureWrapper<T>> Changed;

		protected T defaultValue;
		protected IReadableTexture<T> tex;

		public ReadableTextureWrapper(T defaultValue) {
			this.defaultValue = defaultValue;
		}

		#region interface

		#region IDisposable
		public void Dispose() {
			tex?.Dispose();
			SetTexture(null);
		}
		#endregion

		#region IReadableTexture
		public int Height { get; protected set; } = 1;
		public int Width { get; protected set; } = 1;
		public T this[int x, int y] => (tex != null) ? tex[x, y] : defaultValue;
		#endregion

		public IReadableTexture<T> Value {
			set {
				var prev = tex;
				try {
					SetTexture(value);
					Changed?.Invoke(this);
				} finally {
					prev?.Dispose();
				}
			}
		}
		#endregion

		#region member
		private void SetTexture(IReadableTexture<T> value) {
			tex = value;
			Width = tex != null ? tex.Width : 1;
			Height = tex != null ? tex.Height : 1;
		}
		#endregion
	}
}
