using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour {
    public void Update() {
        if (Keyboard.current.spaceKey.wasPressedThisFrame) {
            Debug.Log("スペースキーが押されたよ");
        }

        if (Mouse.current.leftButton.wasPressedThisFrame) {
            Debug.Log("マウスの左ボタンがクリックされたよ");
        }

        if (Gamepad.current.aButton.wasPressedThisFrame) {
            Debug.Log("ゲームパッドのaボタンがクリックされたよ");
        }
    }
}