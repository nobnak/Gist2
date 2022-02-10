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
		public event System.Action OnRenew;
        public event System.Action AfterRenew;

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
            if (underEvaluation) throw new System.InvalidOperationException("Recursive Assure calls");
            if (lastValidationTime == Time.frameCount) return;
            if (validity && (Examine == null || Examine())) return;

            Renew();
            AfterRenew?.Invoke();
        }

        public void Expire() => validity = false;
		#endregion

		public void Reset() {
			validity = initialValidity;
			Examine = null;
			OnRenew = null;
			lastValidationTime = -1;
			underEvaluation = false;
		}
        #endregion

        #region member
        protected void Renew() {
            try {
                underEvaluation = true;
                OnRenew?.Invoke();
            } finally {
                underEvaluation = false;
                validity = true;
                lastValidationTime = Time.frameCount;
            }
        }
        #endregion
    }
}