using Gist2.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

namespace Gist2.Visualization {

    public class TextureHolder : MonoBehaviour, IValue<Texture> {

        public Texture tex = null;
		public Events events = new Events();

        #region interface
        public Texture Value => tex;
		public void SetTexture(Texture tex) {
			this.tex = tex;
			events.OnSetTexture.Invoke(tex);
		}
        #endregion

        #region static
        public static implicit operator Texture (TextureHolder th) => (th != null) ? th.tex : null;
        #endregion

        #region classes
		[System.Serializable]
		public class Events {
			public TextureEvent OnSetTexture = new TextureEvent();

			[System.Serializable]
			public class TextureEvent : UnityEvent<Texture> { }
		}
        #endregion
    }
}
