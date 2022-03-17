using Gist2.Analysis.Modules;
using Gist2.Deferred;
using Gist2.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Analysis {

    public class Monitoring : MonoBehaviour {

        public Tuner tuner = new Tuner();

        Assurance assurance = new Assurance();
        TargetFramerate targetFramerate;
        AutoHideCursor autoHide;
        FramerateCounter framerate;

        #region unity
        private void OnEnable() {
            targetFramerate = new TargetFramerate();
            autoHide = new AutoHideCursor();
            framerate = new FramerateCounter();

            assurance.Reset();
            assurance.Renew += () => {
                targetFramerate.CurrTuner = tuner.targetFramerate;
                autoHide.CurrTuner = tuner.autoHide;
                framerate.CurrTuner = tuner.framerate;
            };

            assurance.Assure();
        }
        private void OnValidate() {
            assurance.Expire();
        }
        private void Update() {
            assurance.Assure();
            targetFramerate.Update();
            autoHide.Update();
            framerate.Update();
        }
        #endregion

        #region classes
        [System.Serializable]
        public class Tuner {
            public TargetFramerate.Tuner targetFramerate = new TargetFramerate.Tuner();
            public AutoHideCursor.Tuner autoHide = new AutoHideCursor.Tuner();
            public FramerateCounter.Tuner framerate = new FramerateCounter.Tuner();
        }







        #endregion
    }
}
