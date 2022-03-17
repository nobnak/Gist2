using Gist2.Deferred;
using Gist2.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Analysis.Modules {

    public class AutoHideCursor : IUpdate {

        Tuner tuner = new Tuner();
        MouseCursorMonitor cursor;

        Assurance assurance = new Assurance();
        float lastTime;
        Vector2 lastPos;

        public AutoHideCursor() {
            cursor = new MouseCursorMonitor();

            assurance.Renew += () => {
                cursor.Update();
                lastTime = Time.time;
                lastPos = cursor.CurrPos;
            };
        }

        public Tuner CurrTuner {
            get => tuner;
            set {
                tuner = value;
                assurance.Expire();
            }
        }

        public void Update() {
            var t = Time.realtimeSinceStartup;

            assurance.Assure();
            cursor.Update();

            if ((cursor.CurrPos - lastPos).sqrMagnitude > tuner.threshold * tuner.threshold) {
                lastTime = t;
                lastPos = cursor.CurrPos;
            }

            Cursor.visible = ((t - lastTime) <= tuner.wait);
        }

        [System.Serializable]
        public class Tuner {
            public float wait = 3f;
            public float threshold = 3f;
        }
    }
}
