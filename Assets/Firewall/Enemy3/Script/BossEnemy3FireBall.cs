using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigEnemy3System;

/// <summary>
/// ボス敵1の生成したダメージ玉の処理
/// 更新日時:0413
/// </summary>
public class BossEnemy3FireBall : GenerateDamageObjBase {

    [SerializeField, Tooltip("玉の移動量")]
    private int _ballSpeed;

    [SerializeField, Tooltip("玉の発射されるまでの時間")]
    private float _shotTime;

    private Animator _animator;
    private GameObject _player;
    private Vector3 _vector;

    private float _moveCount;


    private void Start() {
        _animator = this.GetComponent<Animator>();
        _player = GameObject.Find("Ridle");
    }//Start
    
    private void FixedUpdate() {
        if (_moveCount == _shotTime+1) {
            transform.position += _vector;
        }//if
    }//FixedUpdate

    private void Update() {
        GenerateDamageObjDestroy();
        if (_moveCount < _shotTime) {
            _moveCount += Time.deltaTime;
        } else if (_moveCount < _shotTime+1) {
            this.gameObject.transform.parent = null;
            AngleSetting();
            _moveCount = _shotTime+1;
            _animator.SetBool("AniMove", true);
        }//if
    }//Update

    /// <summary>
    /// 玉の移動する角度を設定する
    /// </summary>
    private void AngleSetting() {
        // 角度をラジアンに変換
        float rad = (this.transform.eulerAngles.z + 90) * Mathf.Deg2Rad;
        // ラジアンから進行方向を設定
        Vector3 direction = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);
        // 方向に速度を掛け合わせて移動ベクトルを求める
        _vector = direction * _ballSpeed * Time.deltaTime;
    }//AngleSetting

}//BossEnemy3FireBall
