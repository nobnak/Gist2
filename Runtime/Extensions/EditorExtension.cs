using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gist2.Extensions.EditorExt {

    public static class EditorExtension {

#if UNITY_EDITOR
        public static readonly System.Type TYPE_GAME_VIEW = System.Type.GetType("UnityEditor.GameView,UnityEditor");

        static EditorWindow gameView;
        static System.Reflection.PropertyInfo propLowRes;

        public static EditorWindow GameView {
            get {
                if (gameView == null) gameView = EditorWindow.GetWindow(TYPE_GAME_VIEW);
                return gameView;
            }
        }
        public static System.Reflection.PropertyInfo LowResolutionAcpectRatioPropertyOfGameView {
            get {
                if (propLowRes == null) propLowRes = TYPE_GAME_VIEW.GetProperty("lowResolutionForAspectRatios");
                return propLowRes;
            }
        }
#endif

        public static bool LowResolutionAspectRatio {
            get {
#if UNITY_EDITOR
                return (bool)LowResolutionAcpectRatioPropertyOfGameView.GetValue(GameView);
#else
                return false;
#endif
            }
        }        
    }
}
