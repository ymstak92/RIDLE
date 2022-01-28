using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigEnemy3System;

public class BigEnemy3_Crane : MonoBehaviour {

    [SerializeField,Tooltip("��]���̍ő�p�x")]
    private int _angleMax;

    [SerializeField,Tooltip("��]���̍ŏ��p�x")]
    private int _angleMin;

    [SerializeField,Tooltip("�A�[���̈ړ����x")]
    private int _armSpeed;

    [SerializeField]
    private string _craneType;

    private bool _isClockwise;
    private float _angleZValue;//�p�x�l�̕ۑ��p�ϐ�

    private float _randomBoolTimeCount;

    void Update() {
        CraneAngleUpdate(_craneType);
    }//Update

    /// <summary>
    /// �N���[���p�x�X�V����
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
