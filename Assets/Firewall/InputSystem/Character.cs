using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour {
    public void Update() {
        if (Keyboard.current.spaceKey.wasPressedThisFrame) {
            Debug.Log("�X�y�[�X�L�[�������ꂽ��");
        }

        if (Mouse.current.leftButton.wasPressedThisFrame) {
            Debug.Log("�}�E�X�̍��{�^�����N���b�N���ꂽ��");
        }

        if (Gamepad.current.aButton.wasPressedThisFrame) {
            Debug.Log("�Q�[���p�b�h��a�{�^�����N���b�N���ꂽ��");
        }
    }
}