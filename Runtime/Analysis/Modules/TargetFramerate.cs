using Gist2.Deferred;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Analysis.Modules {

    public class TargetFramerate : IUpdate {
        Tuner tuner = new Tuner();
        Assurance assurance = new Assurance();

        public TargetFramerate() {
            assurance.Reset();
            assurance.Renew += () => {
                Application.targetFrameRate = (tuner.framerate >= 0) ? tuner.framerate : -1;
                QualitySettings.vSyncCount = Mathf.Clamp(tuner.vSyncCount, 0, 4);
            };

            assurance.Assure();
        }

        public Tuner CurrTuner {
            get => tuner;
            set {
                tuner = value;
                assurance.Expire();
            }
        }
        public void Update() {
            assurance.Assure();
        }

        [System.Serializable]
        public class Tuner {
            public int framerate = -1;
            public int vSyncCount = 0;
        }
    }
}
