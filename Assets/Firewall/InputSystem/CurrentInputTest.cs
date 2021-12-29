using UnityEngine;
using UnityEngine.InputSystem;
public class CurrentInputTest : MonoBehaviour {
    void Update() {
        var gamepad = Gamepad.current;
        if (gamepad == null)
            return;    // No gamepad connected.
        if (gamepad.rightTrigger.wasPressedThisFrame) {
            Debug.Log($"rightTrigger:{gamepad}");
        }
    }
}