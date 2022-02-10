namespace Gist2.Interfaces {
	public interface IReadableTexture<T> : System.IDisposable where T : struct {
		T this[int x, int y] { get; }
		int Height { get; }
		int Width { get; }
	}
}