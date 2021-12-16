using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Inputs {

    public class Mouse {
        public const int DEFAULT_FRAME = -1;

        protected int currFrame = DEFAULT_FRAME;
        protected Vector2 currPos;
        protected Vector2 currPosDelta;

        protected int prevFrame;
        protected Vector2 prevPos;

        public Mouse() {
            Validate();
        }

        #region interface

        #region object
        public override string ToString()
            => $"<{GetType().Name} : pos={currPos}, delta={currPosDelta} at {currFrame}>";
        #endregion

        public Vector2 Delta {
            get {
                Validate();
                return currPosDelta;
            }
        }
        public Vector2 CurrPos {
            get {
                Validate();
                return currPos;
            }
        }
        public Vector2 PrevPos {
            get {
                Validate();
                return prevPos;
            }
        }

        public void Validate() {
            var nextFrame = Time.frameCount;
            if (currFrame != nextFrame) {
                prevFrame = currFrame;
                currFrame = nextFrame;

                prevPos = currPos;
                currPos = GetCurrentPosition();
                if (prevFrame == DEFAULT_FRAME) prevPos = currPos;
                currPosDelta = currPos - prevPos;
            }
        }
        public void Update() => Validate();
        #endregion

        #region static
        public static Vector2 GetCurrentPosition() => (Vector2)Input.mousePosition;
        #endregion
    }

}
