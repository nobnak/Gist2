using Gist2.Analysis.Modules;
using Gist2.Deferred;
using Gist2.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Analysis {

    public class Monitoring : MonoBehaviour {

        public Tuner tuner = new Tuner();

        Validator assurance = new Validator();
        TargetFramerate targetFramerate;
        AutoHideCursor autoHide;
        FramerateCounter framerate;

        #region unity
        private void OnEnable() {
            targetFramerate = new TargetFramerate();
            autoHide = new AutoHideCursor();
            framerate = new FramerateCounter();

            assurance.Reset();
            assurance.OnValidate += () => {
                targetFramerate.CurrTuner = tuner.targetFramerate;
                autoHide.CurrTuner = tuner.autoHide;
                framerate.CurrTuner = tuner.framerate;
            };

            assurance.Validate();
        }
        private void OnValidate() {
            assurance.Invalidate();
        }
        private void Update() {
            assurance.Validate();
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
