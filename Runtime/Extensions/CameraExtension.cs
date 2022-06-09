using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Extensions.CameraExt {

	public static class CameraExtension {

		public static float VisibleHeight(this Camera c, Vector3 target = default) {
			float height;

			if (c.orthographic) {
				height = c.orthographicSize;
			} else {
				var z = -c.transform.InverseTransformPoint(target).z;
				height = z * Mathf.Tan(0.5f * c.fieldOfView);
			}

			height = Mathf.Max(0f, height);
			return 2f * height;
		}
	}
}
