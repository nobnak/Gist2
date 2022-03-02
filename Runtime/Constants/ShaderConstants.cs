using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gist2.Constants {

    public static class ShaderConstants {

		public enum ZTestEnum {
			NEVER = 1, LESS = 2, EQUAL = 3, LESSEQUAL = 4,
			GREATER = 5, NOTEQUAL = 6, GREATEREQUAL = 7, ALWAYS = 8
		};
		public enum CullEnum {
			None = 0, Front = 1, Back = 2
		};
		[System.Flags]
		public enum ColorMaskEnum {
			None = 0,
			A = 1,
			B = 1 << 1,
			G = 1 << 2,
			R = 1 << 3,
			RGB = R | G | B,
			ALL = RGB | A
		}
	}
}
