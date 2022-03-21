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
		public event System.Action Expiration;

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
            if (validity 
				&& ((lastValidationTime == Time.frameCount) || (Examine == null || Examine()))) 
				return;

            try {
                TransactOfRenew();
            } finally {
                AfterRenew?.Invoke();
            }
        }

		public void Expire() {
			validity = false;
			Expiration?.Invoke();
		}
		#endregion

		public void Reset() {
			validity = initialValidity;
			Expiration = null;
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