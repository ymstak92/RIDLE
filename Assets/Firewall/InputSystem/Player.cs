using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    Vector3 move;
    RIDLEDemo testInputActions;

    void Awake() {
        testInputActions = new RIDLEDemo();
        testInputActions.Enable();
        testInputActions.Player.Fire.performed += context => Debug.Log("Fire");
    }

    void Update() {
        const float Speed = 1f;

        var direction = testInputActions.Player.Move.ReadValue<Vector2>();
        //Debug.Log("x_" + direction.x + "_y_" + direction.y);
        transform.Translate(direction * Speed * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context) {
        move = context.ReadValue<Vector2>();
        //Debug.Log("x_"+move.x+ "_y_" + move.y+ "_z_" + move.z);
    }

    public void OnFire(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed) {
            //Debug.Log("Fire");
        }
    }

}