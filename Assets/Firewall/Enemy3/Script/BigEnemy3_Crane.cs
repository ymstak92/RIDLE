using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigEnemy3System;

public class BigEnemy3_Crane : MonoBehaviour {

    [SerializeField,Tooltip("回転時の最大角度")]
    private int _angleMax;

    [SerializeField,Tooltip("回転時の最小角度")]
    private int _angleMin;

    [SerializeField,Tooltip("アームの移動速度")]
    private int _armSpeed;

    [SerializeField]
    private string _craneType;

    private bool _isClockwise;
    private float _angleZValue;//角度値の保存用変数

    private float _randomBoolTimeCount;

    void Update() {
        CraneAngleUpdate(_craneType);
    }//Update

    /// <summary>
    /// クレーン角度更新処理
    /// </summary>
    private void CraneAngleUpdate(string craneType) {
        switch (craneType) {
            case "regular":
                break;
            case "random":
                (_isClockwise, _randomBoolTimeCount) = BigEnemy3.RandomBool(_isClockwise, 1, _randomBoolTimeCount);
                break;
            default:
                break;
        }//switch
        switch (_isClockwise) {
            case true:
                _angleZValue -= Time.deltaTime * _armSpeed;
                if (BigEnemy3.AngleNotationChange(this.transform.eulerAngles.z) < _angleMin) {
                    _isClockwise = false;
                }//if
                break;
            case false:
                _angleZValue += Time.deltaTime * _armSpeed;
                if (BigEnemy3.AngleNotationChange(this.transform.eulerAngles.z) > _angleMax) {
                    _isClockwise = true;
                }//if
                break;
        }//switch
        this.transform.localRotation = Quaternion.Euler(0, 0, _angleZValue);
    }//CraneAngleUpdate

}//BigEnemy3_Crane
