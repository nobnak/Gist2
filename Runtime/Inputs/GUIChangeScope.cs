using Gist2.Extensions.LinqExt;

namespace Gist2.Inputs {

    public class GUIChangeScope : System.IDisposable {
        public event System.Action Changed;
        public bool prevState;

        public GUIChangeScope(params System.Action[] changed) {
            changed.ForEach(v => Changed += v);
            prevState = IsChanged;
            IsChanged = false;
        }

        public void Dispose() {
            if (IsChanged) Changed?.Invoke();
            IsChanged = IsChanged || prevState;
        }

        public static bool IsChanged {
            get => UnityEngine.GUI.changed;
            set => UnityEngine.GUI.changed = value;
        }
        public static implicit operator bool (GUIChangeScope c) => IsChanged;
    }
}