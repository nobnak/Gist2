using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Gist2.Extensions.Maths {

	public static class QuantizeExt {

		public static float Quantize2_Ceil(this float v, float sig_digits2) {
			var d = (int)math.log2(v) + 1 - sig_digits2;
			var b = math.exp2(d);
			return math.ceil(v / b) * b;
		}
		public static float Quantize2(this float v, float sig_digits2) {
			var s = math.sign(v);
			v *= s;
			var d = (int)math.log2(v) + 1 - sig_digits2;
			var b = math.exp2(d);
			return s * math.floor(v / b) * b;
		}

		public static float Quantize10_Ceil(this float v, float sig_digits10) {
			var d = (int)math.log10(v) + 1 - sig_digits10;
			var b = math.exp10(d);
			return math.ceil(v / b) * b;
		}
		public static float Quantize10(this float v, float sig_digits10) {
			var s = math.sign(v);
			v *= s;
			var d = (int)math.log10(v) + 1 - sig_digits10;
			var b = math.exp10(d);
			return s * math.floor(v / b) * b;
		}
	}
}
