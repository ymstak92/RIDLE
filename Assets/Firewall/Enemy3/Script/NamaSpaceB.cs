using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NamespaceTest1;
using NamespaceTest1.NamespaceTest2;

public class NamaSpaceB : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        Debug.LogError("NamaSpaceB");
        NameSpaceAA nameSpaceAA = GetComponent<NameSpaceAA>();
        NameSpaceA.test();
    }

    // Update is called once per frame
    void Update() {

    }
}
