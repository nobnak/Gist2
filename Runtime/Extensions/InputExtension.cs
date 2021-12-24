using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gist2.Extensions.InputExt {

    public static class InputExtension {

        public static Vector2 GetMousePosition() {
            return UnityEngine.InputSystem.Mouse.current.position.ReadValue();
        }

        public static bool GetKeyDown(this Key k) {
            return Keyboard.current[k].wasPressedThisFrame;
        }
    }

}
