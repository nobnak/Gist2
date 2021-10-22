using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Deferred {

    public interface IInitializable {
        void Initialize();
    }

    public struct Init : IInitializable {

        public event System.Action Initialization;

        public event System.Action AfterReset;
        public event System.Action AfterInitialize;

        private bool initialized;

        public Init(bool initialized = false) {
            this.initialized = initialized;

            this.AfterInitialize = null;
            this.AfterReset = null;
            this.Initialization = null;
        }

        public void Initialize() {
            if (initialized) return;
            initialized = true;
            try {
                Initialization?.Invoke();
                AfterInitialize?.Invoke();
            } catch (System.Exception e) {
                Debug.LogWarning(e);
            }
        }
        public void Reset() {
            if (!initialized) return;
            initialized = false;
            try {
                AfterReset?.Invoke();
            } catch (System.Exception e) {
                Debug.LogWarning(e);
            }
        }
    }
}