namespace Gist2.Interfaces {

	public interface IValue<out T> {

        T Value { get; }
    }
}