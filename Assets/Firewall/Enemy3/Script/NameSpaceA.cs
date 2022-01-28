using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NamespaceTest1 {

    public class NameSpaceAA : MonoBehaviour {
        // Start is called before the first frame update
        void Start() {
            Debug.Log("NameSpaceAA");
        }

        // Update is called once per frame
        void Update() {

        }
    }

    namespace NamespaceTest2 {
        public static class NameSpaceA {
            
            public static void test() {
                Debug.Log("ssss");
                //count++;
                //Debug.Log(count);
            }
        }
    }
}


