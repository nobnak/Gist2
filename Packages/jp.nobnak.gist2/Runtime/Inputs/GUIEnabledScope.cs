using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Inputs {

    public class GUIEnabledScope : System.IDisposable {

        protected bool prevState;

        public GUIEnabledScope(bool enabled) {
            prevState = GUI.enabled;
            GUI.enabled = enabled;
        }

        public void Dispose() {
            GUI.enabled = prevState;
        }
    }
}
