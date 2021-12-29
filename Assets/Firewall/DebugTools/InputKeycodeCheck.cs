using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputKeycodeCheck : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.anyKeyDown) {
            foreach (KeyCode code in Enum.GetValues(typeof(KeyCode))) {
                if (Input.GetKeyDown(code)) {
                    //ˆ—‚ğ‘‚­
                    Debug.Log(code);
                    break;
                }
            }
        }
    }
}
