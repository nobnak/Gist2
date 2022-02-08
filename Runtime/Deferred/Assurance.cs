using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Deferred {

    public interface IAssurance {
        void Assure();
		void Expire();
    }

    public class Assurance : IAssurance {

		public event System.Func<bool> Examine;
		public event System.Action Renew;

		protected bool initialValidity;
		protected bool validity;
		protected bool underEvaluation;
		protected int lastValidationTime = -1;

        public Assurance(bool validity = false) {
			this.initialValidity = validity;
			Reset();
		}

		#region interface

		#region IAssurance
		public void Assure() {
			if (underEvaluation)
				throw new System.InvalidOperationException("Recursive Assure calls");
			if (lastValidationTime == Time.frameCount) return;
			if (validity && (Examine == null || Examine())) return;

			try {
				underEvaluation = true;
				Renew?.Invoke();
			} finally {
				underEvaluation = false;
				validity = true;
				lastValidationTime = Time.frameCount;
			}
        }
        public void Expire() {
            if (!validity) return;
            validity = false;
        }
		#endregion

		public void Reset() {
			validity = initialValidity;
			Examine = null;
			Renew = null;
			lastValidationTime = -1;
			underEvaluation = false;
		}
		#endregion
	}
}