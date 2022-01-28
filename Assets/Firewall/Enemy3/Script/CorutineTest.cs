using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorutineTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {

        
    }
    private float count=2;
    private float counter;
    // Update is called once per frame
    void Update()
    {
        
    }
    public bool RamdomBool(bool isBool,float ramdomCount,float counter) {
        if (counter > ramdomCount) {
            counter = 0;
            return Random.Range(0, 2) == 0;
        } else {
            counter += Time.deltaTime;
        }
        return isBool;
    }
}
