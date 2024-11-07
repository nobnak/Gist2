using Gist2.Interfaces;
using Unity.Collections;
using UnityEngine;

namespace Gist2.Adapter {

	public class NativeArrayTexture<T> : System.IDisposable, IReadableTexture<T> where T : struct {

		public NativeArrayTexture(NativeArray<T> src, int width, int height) {
			this.Data = src;
			this.Width = width;
			this.Height = height;
		}
		public NativeArrayTexture(NativeArray<T> src, Vector2Int size) : this(src, size.x, size.y) { }

		#region interface

		#region IDisposable
		public void Dispose() {}
		#endregion

		#region ITexture
		public int Width { get; protected set; }
		public int Height { get; protected set; }
		public T this[int x, int y] => Data[x + y * Width];
		#endregion

		public NativeArray<T> Data { get; protected set; }
		#endregion
	}	
}
