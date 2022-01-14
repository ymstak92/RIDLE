using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteEnable : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer[] spriterendereTest;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            foreach (SpriteRenderer sr in spriterendereTest)
                sr.enabled = !sr.enabled;
        }
    }
}
