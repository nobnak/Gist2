using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gist2.Extensions.InputExt {

    public static class InputExtension {

        public static Vector2 GetMousePosition() {
            var currMouse = UnityEngine.InputSystem.Mouse.current;
            return (currMouse == null) ? default : currMouse.position.ReadValue();
        }

        public static bool GetKeyDown(this Key k) {
            var currKeyboard = Keyboard.current;
            return (currKeyboard == null) ? default : Keyboard.current[k].wasPressedThisFrame;
        }
    }

}
