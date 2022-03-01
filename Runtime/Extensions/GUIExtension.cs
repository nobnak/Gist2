using UnityEngine;

namespace Gist2.GUIExt {

	public static class GUIExtension {
		public const char CHAR_OPEN = '▼';
		public const char CHAR_CLOSE = '▶';

		static GUIStyle styleFoldout = null;

		#region interface

		#region static
		public static GUIStyle FoldoutStyle { 
			get {
				if (styleFoldout == null) {
					styleFoldout = new GUIStyle(UnityEngine.GUI.skin.label) {
						alignment = TextAnchor.MiddleLeft
					};
					var coff = FoldoutStyle.normal.textColor;
					var con = Color.white;

					styleFoldout.onNormal.textColor
						= FoldoutStyle.onHover.textColor
						= FoldoutStyle.active.textColor
						= con;

					styleFoldout.normal.textColor
						= FoldoutStyle.hover.textColor
						= FoldoutStyle.onActive.textColor
						= coff;
				}

				return styleFoldout;
			}
		}

		public static bool FoldOut(this bool visible, string title, params GUILayoutOption[] options) {
			var foldoutTitle = (visible ? CHAR_OPEN : CHAR_CLOSE) + title;
			return GUILayout.Toggle(visible, foldoutTitle, FoldoutStyle, options);
		}


		#endregion
		#endregion
	}
}
