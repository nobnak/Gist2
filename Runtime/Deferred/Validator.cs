using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Deferred {

    public interface IValidator {
        void Validate(bool force = false);
		void Invalidate();
    }

    public class Validator : IValidator {

		public event System.Func<bool> CheckValidity;
		public event System.Action OnValidate;
        public event System.Action AfterValidate;
		public event System.Action OnInvalidate;

		protected bool initialValidity;
		protected bool validity;
		protected bool underEvaluation;
		protected int lastValidationTime = -1;

        public Validator(bool validity = false) {
			this.initialValidity = validity;
			Reset();
		}

		#region interface

		#region IAssurance
		public void Validate(bool force = false) {
            if (force) Invalidate();
            if (underEvaluation) throw new System.InvalidOperationException("Recursive Assure calls");
            if (validity 
				&& ((lastValidationTime == Time.frameCount) || (CheckValidity == null || CheckValidity()))) 
				return;

            try {
                TransactOfRenew();
            } finally {
                AfterValidate?.Invoke();
            }
        }

		public void Invalidate() {
			validity = false;
			OnInvalidate?.Invoke();
		}
		#endregion

		public void Reset() {
			validity = initialValidity;
			OnInvalidate = null;
			CheckValidity = null;
			OnValidate = null;
			lastValidationTime = -1;
			underEvaluation = false;
		}
        #endregion

        #region member
        protected void TransactOfRenew() {
            try {
                underEvaluation = true;
                OnValidate?.Invoke();
            } finally {
                underEvaluation = false;
                validity = true;
                lastValidationTime = Time.frameCount;
            }
        }
        #endregion
    }
}