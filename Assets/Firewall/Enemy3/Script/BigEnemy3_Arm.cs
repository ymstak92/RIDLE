using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigEnemy3System;

public class BigEnemy3_Arm : MonoBehaviour {

    private readonly float ANGLE_MAX = 120;//�A�[���̍ő�p�x 
    private readonly float ANGLE_MIN = -120;//�A�[���̍ŏ��p�x

    private GameObject _player;//�v���C���[�I�u�W�F�N�g

    private float _armSpeed;
    private float _angleZValue;//�p�x�l�̕ۑ��p�ϐ�
    private float _randomBoolTimeCount;
    private float _fireShotTimeCount;

    private bool _isRandomBool;

    [SerializeField,Tooltip("�A�[���̋����^�C�v(Search��Ramdom���L��)")]
    private string armType;

    void Start() {
        _player = GameObject.Find("Ridle");
        _armSpeed = 70;
        _angleZValue = AngleFaceTarget(_player.transform.position);
        GameObject gO;
        gO= BigEnemy3.RootObjectSearch(this.gameObject);
    }//Start



    void Update() {
        ArmAngleUpdate(armType);
        FireShot();
    }//Update

    /// <summary>
    /// �A�[���p�x�X�V����
    /// </summary>
    private void ArmAngleUpdate(string armType) {
        float thisAngleZ = BigEnemy3.AngleNotationChange(this.transform.eulerAngles.z);
        float parentAngleZ = BigEnemy3.AngleNotationChange(this.transform.parent.eulerAngles.z);
        float currentAngleZ = BigEnemy3.AngleNotationChange(thisAngleZ - parentAngleZ);
        switch (armType) {
            case "Search":
                ArmAngleUpdate_SearchType(currentAngleZ, thisAngleZ);
                break;
            case "Ramdom":
                ArmAngleUpdate_RamdomType(currentAngleZ);
                break;
        }//switch
        this.transform.localRotation = Quaternion.Euler(0, 0, _angleZValue);
    }//SearchArmAngleUpdate

    private void ArmAngleUpdate_SearchType(float currentAngleZ, float thisAngleZ) {
        float targetAngleZ = AngleFaceTarget(_player.transform.position);
        if (thisAngleZ > targetAngleZ) {
            if (currentAngleZ > ANGLE_MIN) {
                _angleZValue -= Time.deltaTime * _armSpeed;
            }//if
        } else {
            if (currentAngleZ < ANGLE_MAX) {
                _angleZValue += Time.deltaTime * _armSpeed;
            }//if
        }//if
    }//ArmAngleUpdate_SearchType

    private void ArmAngleUpdate_RamdomType(float currentAngleZ) {
        (_isRandomBool, _randomBoolTimeCount) = BigEnemy3.RandomBool(_isRandomBool, (float)0.5, _randomBoolTimeCount);
        if (_isRandomBool) {
            if (currentAngleZ > 0) {
                _angleZValue -= Time.deltaTime * _armSpeed;
            }//if
        } else {
            if (currentAngleZ < ANGLE_MAX/2) {
                _angleZValue += Time.deltaTime * _armSpeed;
            }//if
        }//if
    }//ArmAngleUpdate_RamdomType

    /// <summary>
    /// �ΏۂɌ������߂̊p�x�����߂鏈��
    /// </summary>
    /// <param name="targetPosition">�p�x�����߂邽�߂̑Ώ�</param>
    /// <returns>�ΏۂɌ������߂̊p�x</returns>
    private float AngleFaceTarget(Vector2 targetPosition) {
        Vector2 arrowPosition = this.transform.position;
        Vector2 dt = targetPosition - arrowPosition;
        float rad = Mathf.Atan2(dt.x, dt.y);
        float angle = -(rad * Mathf.Rad2Deg) % 360;//�p�x�擾
        return angle;
    }//AngleFaceTarget

    private void FireShot() {
        _fireShotTimeCount += Time.deltaTime;
        if (_fireShotTimeCount > 5) {
            GameObject instance = (GameObject)Instantiate(
                (GameObject)Resources.Load("GameObject/BossEnemy3FireBall"), 
                new Vector2(this.transform.GetChild(0).position.x, this.transform.GetChild(0).position.y), Quaternion.identity);
            instance.transform.parent = this.gameObject.transform;
            instance.transform.rotation = this.gameObject.transform.rotation;
            _fireShotTimeCount = 0;
        }
    }

}//BigEnemy3_Arm
