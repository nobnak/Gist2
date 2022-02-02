using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace Gist2.Extensions.InputExt {

    public static class InputExtension {

        public static Vector2 GetMousePosition() {
#if ENABLE_INPUT_SYSTEM
            var currMouse = UnityEngine.InputSystem.Mouse.current;
            return (currMouse == null) ? default : currMouse.position.ReadValue();
#else
            return Input.mousePosition;
#endif
        }


#if ENABLE_INPUT_SYSTEM
        public static bool GetKeyDown(this Key k) {
            var currKeyboard = Keyboard.current;
            return (currKeyboard == null) ? default : Keyboard.current[(Key)k].wasPressedThisFrame;
        }
#else
        public static bool GetKeyDown(this KeyCode k) => Input.GetKeyDown((KeyCode)k);
#endif

        public static bool GetKeyDown(this string key) {
#if ENABLE_INPUT_SYSTEM
            return System.Enum.TryParse<Key>(key, out var k) && k.GetKeyDown();
#else
            return System.Enum.TryParse<KeyCode>(key, out var k) && k.GetKeyDown();
#endif
        }
    }
}
