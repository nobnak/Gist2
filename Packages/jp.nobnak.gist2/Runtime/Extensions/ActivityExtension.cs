using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Extensions.ActivityExt {


	public static class ActivityExtension {

		public static bool IsActive(this MonoBehaviour b) {
			var res = b != null && b.isActiveAndEnabled;
			#if UNITY_EDITOR
			res = res && (Application.isPlaying || b.runInEditMode);
			#endif
			return res;
		}

		public static bool IsVisible(this GameObject g) {
			var c = Camera.current;
			return g != null && g.activeInHierarchy
				&& c != null && (c.cullingMask & (1 << g.layer)) != 0;
		}
		public static bool IsVisible(this Component c)
			=> (c == null) ? false : c.gameObject.IsVisible();

		public static bool IsActiveAndVisible(this MonoBehaviour b)
			=> b.IsActive() && b.IsVisible();
	}
}
