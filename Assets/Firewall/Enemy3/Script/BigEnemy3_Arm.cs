using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigEnemy3System;

public class BigEnemy3_Arm : MonoBehaviour {

    private readonly float ANGLE_MAX = 120;//アームの最大角度 
    private readonly float ANGLE_MIN = -120;//アームの最小角度

    private GameObject _player;//プレイヤーオブジェクト

    private float _armSpeed;
    private float _angleZValue;//角度値の保存用変数
    private float _randomBoolTimeCount;
    private float _fireShotTimeCount;

    private bool _isRandomBool;

    [SerializeField,Tooltip("アームの挙動タイプ(SearchかRamdomを記載)")]
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
    /// アーム角度更新処理
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
    /// 対象に向くための角度を求める処理
    /// </summary>
    /// <param name="targetPosition">角度を求めるための対象</param>
    /// <returns>対象に向くための角度</returns>
    private float AngleFaceTarget(Vector2 targetPosition) {
        Vector2 arrowPosition = this.transform.position;
        Vector2 dt = targetPosition - arrowPosition;
        float rad = Mathf.Atan2(dt.x, dt.y);
        float angle = -(rad * Mathf.Rad2Deg) % 360;//角度取得
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
