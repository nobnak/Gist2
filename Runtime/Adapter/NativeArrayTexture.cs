using Gist2.Interfaces;
using Unity.Collections;

namespace Gist2.Adapter {

	public class NativeArrayTexture<T> : System.IDisposable, IReadableTexture<T> where T : struct {

		protected NativeArray<T> data;

		public NativeArrayTexture(NativeArray<T> src, int width, int height) {
			this.data = src;
			this.Width = width;
			this.Height = height;
		}

		#region interface

		#region IDisposable
		public void Dispose() {
			data.Dispose();
		}
		#endregion

		#region ITexture
		public int Width { get; protected set; }
		public int Height { get; protected set; }
		public T this[int x, int y] => data[x + y * Width];
		#endregion

		#endregion
	}	
}
