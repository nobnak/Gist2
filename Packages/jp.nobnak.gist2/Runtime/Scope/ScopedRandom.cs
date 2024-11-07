using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Scope {

    public class ScopedRandom : System.IDisposable {

        public readonly Random.State pastState;

        public ScopedRandom(int seed) {
            pastState = Random.state;
            Random.InitState(seed);
        }

        public void Dispose() {
            Random.state = pastState;
        }
    }
}
