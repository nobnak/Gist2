using Gist2.Deferred;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Analysis.Modules {

    public class FramerateCounter : IUpdate {

        Tuner tuner = new Tuner();
        Assurance assurance;
        float lastTime;
        int lastFrameCount;
        float currFramerate;

        public FramerateCounter() {
            assurance = new Assurance();
            assurance.Renew += () => {
                currFramerate = 0f;
                lastTime = Time.time;
                lastFrameCount = Time.frameCount;
            };
            assurance.Assure();
        }

        public void Update() {
            var t = Time.time;
            var c = Time.frameCount;

            assurance.Assure();

            var dt = t - lastTime;
            var df = c - lastFrameCount;
            if (dt > tuner.duration && df > tuner.frames) {
                currFramerate = df / dt;
                lastTime = t;
                lastFrameCount = c;
                Debug.Log($"{this}");
            }
        }

        public Tuner CurrTuner {
            get => tuner;
            set {
                tuner = value;
                assurance.Expire();
            }
        }
        public float CurrFramerate { get => currFramerate; }
        public override string ToString() => $"{currFramerate:f1} fps";

        [System.Serializable]
        public class Tuner {
            public float duration = 3f;
            public int frames = 100;
        }
    }
}
