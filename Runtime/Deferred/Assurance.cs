using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Deferred {

    public interface IAssurance {
        void Assure(bool force = false);
		void Expire();
    }

    public class Assurance : IAssurance {

		public event System.Func<bool> Examine;
		public event System.Action Renew;
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
		public void Assure(bool force = false) {
            if (force) Expire();
            if (underEvaluation) throw new System.InvalidOperationException("Recursive Assure calls");
            if (lastValidationTime == Time.frameCount) return;
            if (validity && (Examine == null || Examine())) return;

            try {
                TransactOfRenew();
            } finally {
                AfterRenew?.Invoke();
            }
        }

        public void Expire() => validity = false;
		#endregion

		public void Reset() {
			validity = initialValidity;
			Examine = null;
			Renew = null;
			lastValidationTime = -1;
			underEvaluation = false;
		}
        #endregion

        #region member
        protected void TransactOfRenew() {
            try {
                underEvaluation = true;
                Renew?.Invoke();
            } finally {
                underEvaluation = false;
                validity = true;
                lastValidationTime = Time.frameCount;
            }
        }
        #endregion
    }
}