using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 移動する床の処理
/// 更新日時:20211221
/// </summary>
public class MovingFloor : MovingGimmick {

    private new void Start() {
        _movingObject = transform.parent.gameObject;
        base.Start();
    }//Start

    private void FixedUpdate() {
        (_xPositionNow, _yPositionNow)= Move(_movingObject, _xPositionNow, _yPositionNow);
    }//Update

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.name != "UnderTrigger")
            return;
        if (col.transform.parent.name != "Ridle")
            return;
        col.transform.parent.SetParent(_movingObject.transform);
    }//OnTriggerEnter2D


    private void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.name != "UnderTrigger")
            return;
        if (col.transform.parent.name != "Ridle")
            return;
        col.transform.parent.SetParent(null);
    }//OnTriggerExit2D

}//MovingFloor
