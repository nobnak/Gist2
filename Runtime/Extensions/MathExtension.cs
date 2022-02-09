using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Extensions.MathExt {

	public static class MathExtension {

		public static float Clamp(this float v, float min, float max) => Mathf.Clamp(v, min, max);
	}
}
