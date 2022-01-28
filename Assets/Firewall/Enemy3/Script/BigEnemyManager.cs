using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボス敵の共通処理をまとめて記載する
/// 更新日時:20220114
/// </summary>
public class BigEnemyManager : MonoBehaviour {

    [Tooltip("敵ごとの最大体力を指定する")]
    [SerializeField]
    private int maxLifeNum =1;//最大体力値

    [SerializeField, Tooltip("当たり判定やダメージ委判定を変更するオブジェクト_オブジェクト確定後Findに変更")]
    private GameObject enemyTrigger;

    private readonly float RECOVERY_TIME = (float)2;//回復するための時間

    public Animator Animator { get; set; }
    public GameObject EnemyMain { get; set; }
    public GameObject EnemyTrigger { get; set; }//用途レイヤーの変更,スクリプト情報の取得のみ
    public GameObject Player { get; set; }
    public Rigidbody2D Rigidbody { get; set; }

    public int NowLifeNum { get; set; }

    public bool IsAppearanceEnd { get; set; }

    [SerializeField,Tooltip("戦闘中に画面上に表示させる画像配列(ミス時に点滅などをさせる)")]
    private SpriteRenderer[] _displaySpriteArray;

    protected AudioManager _audioManager;
    private CameraChase _cameraChase;
    private EnemyBodyTrigger _enemyBodyTrigger;
    protected StageStatusManagement _stageClearManagement;
    private Vector2 _mainCameraTF;

    private float _destroyPositionY;//ミス時のゲームオブジェクト消去position.Y;
    private float _recoveryTimer;//回復までの現在時間
    private float _rendererEnableTime;//ダメージ中の画像表示・非表示の切り替え時間 時間を更新していくので定数にはしない

    private string _stageBGMName;


    public void Start() {
        Animator = this.GetComponent<Animator>();
        EnemyTrigger = enemyTrigger;
        Player = GameObject.Find("Ridle");
        Rigidbody = this.GetComponent<Rigidbody2D>();

        NowLifeNum = maxLifeNum;

        _audioManager = GameObject.Find("GameManager").GetComponent<AudioManager>();
        _cameraChase = Camera.main.GetComponent<CameraChase>();
        _enemyBodyTrigger = EnemyTrigger.GetComponent<EnemyBodyTrigger>();
        _stageClearManagement = GameObject.Find("Stage").GetComponent<StageStatusManagement>();
        _mainCameraTF = GameObject.Find("Main Camera").GetComponent<Transform>().position;

        _recoveryTimer = RECOVERY_TIME;

        _stageBGMName = _audioManager.BGMAudio.clip.name;
    }//Start

    /// <summary>
    /// ボス戦共通のUpdate処理
    /// </summary>
    public void ParentUpdate() {
        
        EnemyDamage();
        EnemyRecovery();
        EnemyMiss();
        if (IsAppearanceEnd) {
            StartCoroutine(BattleStartCorutine());
        }//if
    }//ParentUpdate

    /// <summary>
    /// ボス戦を始めるまでの処理
    /// </summary>
    /// <returns></returns>
    IEnumerator BattleStartCorutine() {
        yield return new WaitForSeconds(1f);
        _cameraChase.IsBossBattle = true;
    }//BattleStartCorutine

    /// <summary>
    /// ダメージを受けたときの処理
    /// </summary>
    private void EnemyDamage() {
        if (_enemyBodyTrigger.EnemyDamageType == EnemyDamageType.None)
            return;
        LayerChange.ParentAndChildChange(this.gameObject, "DamageBigEnemy");
        _recoveryTimer = 0;
        _rendererEnableTime = 0;
        NowLifeNum--;
        if (NowLifeNum == 0) {
            DamageEffectSelect();
        }//if
        if (_recoveryTimer == 0) {
            _audioManager.PlaySE("EnemyMiss");
        }//if
        _enemyBodyTrigger.EnemyDamageType = EnemyDamageType.None;
    }//EnemyDamage

    /// <summary>
    /// ダメージエフェクトを選択する処理
    /// </summary>
    private void DamageEffectSelect() {
        Vector2 generatePos;
        switch (_enemyBodyTrigger.EnemyDamageType) {
            case EnemyDamageType.Slashing:
                generatePos = new Vector2(
                    this.transform.position.x + this.GetComponent<Collider2D>().offset.x,
                    this.transform.position.y + this.GetComponent<Collider2D>().offset.y);
                AttackEffect.EffectGenerate("GameObject/SlashingDamage", generatePos, this.gameObject, true);
                break;
            case EnemyDamageType.Trampling:
                generatePos = new Vector2(
                    Player.transform.position.x,
                    Player.transform.position.y);
                AttackEffect.EffectGenerate("GameObject/ShockWave", generatePos, Player, false);
                break;
            default:
                break;
        }//switch
    }//DamageEffectSelect


    /// <summary>
    /// ダメージを受けた後の処理
    /// </summary>
    private void EnemyRecovery() {
        if (NowLifeNum == 0 ||//体力がない場合
            (_recoveryTimer > RECOVERY_TIME))//回復済みの場合
            return;
        _recoveryTimer += Time.deltaTime;
        if (_recoveryTimer >= RECOVERY_TIME) {//回復する場合
            SpritesEnable(true);
            LayerChange.ParentAndChildChange(this.gameObject, "BigEnemy");
            LayerChange.ParentChange(EnemyTrigger, "EnemyAttack");
            return;
        }//if
        SpriteRendererEnable();
    }//EnemyRecovery

    /// <summary>
    /// 画像の表示・非表示についての処理 
    /// EnemyRecovery EnemyMissで使用
    /// </summary>    
    private void SpriteRendererEnable() {
        if (_recoveryTimer <= _rendererEnableTime)
            return;
        SpritesEnable(!_displaySpriteArray[0].enabled);
        _rendererEnableTime += (float)0.1;
    }//SpriteRendererEnable

    /// <summary>
    /// Spriteの有効化無効化一括変更処理
    /// </summary>
    /// <param name="isEnable"></param>
    private void SpritesEnable(bool isEnable) {
        foreach (SpriteRenderer sprite in _displaySpriteArray) {
            sprite.enabled = isEnable;
        }//foreach
    }//SpritesEnable

    /// <summary>
    /// ミスしたとき(体力がなくなったとき)の処理   
    /// </summary>
    private void EnemyMiss() {
        if (NowLifeNum != 0)
            return;
        if (_recoveryTimer == 0) {
            _audioManager.PlaySE("BossEnemyMiss");
        }//if
        _stageClearManagement.StageStatus = EnumStageStatus.AfterBossBattle;
        _recoveryTimer += Time.deltaTime;
        Animator.SetBool("AniMiss", true);
        LayerChange.ParentAndChildChange(this.gameObject, "Not Interference");
        SpriteRendererEnable();
        StartCoroutine("MissEnumerator");
    }//EnemyMiss



    /// <summary>
    /// ミス時の落下処理(1.5秒後)
    /// EnemyMissで使用
    /// </summary>
    /// <returns></returns>
    IEnumerator MissEnumerator() {
        yield return new WaitForSeconds((float)2.5);
        _destroyPositionY = _mainCameraTF.y - 25;
        if (this.transform.position.y > _destroyPositionY) {
            this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - (Time.deltaTime * (float)20));
        } else {//画面内から出た場合
            try {
                _stageClearManagement.BossEnemyArray.RemoveAt(0);
                _stageClearManagement.StageStatus = EnumStageStatus.Normal;
                _audioManager.PlayBGM(_stageBGMName);
                Destroy(this.gameObject);
            } catch (Exception) {
                Debug.LogError("ボス敵を削除するときにエラー発生");
            }//try
        }//if
    }//missEnumerator

    /// <summary>
    /// animatorのbool型変更処理
    /// </summary>
    /// <param name="boolName"></param>
    public void AnimatorChangeBool(string boolName, bool boolean) {
        if (Animator.GetBool(boolName) != boolean) {
            Animator.SetBool(boolName, boolean);
        }//if
    }//AnimatorChangeBool

    /// <summary>
    /// プレイヤーの方向を向く処理
    /// </summary>
    protected void TurnToThePlayer() {
        if (Player.transform.position.x < this.transform.position.x) {//自機が左にいる場合
            this.transform.localScale = new Vector2(-Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y);
        } else {
            this.transform.localScale = new Vector2(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y);
        }//if
    }//TurnToThePlayer

    /// <summary>
    /// BGM変更までの停止処理
    /// </summary>
    /// <param name="corutineTime"></param>
    /// <returns></returns>
    protected IEnumerator BGMCorutine(float corutineTime) {
        yield return new WaitForSeconds(corutineTime);
        _audioManager.PlayBGM("BossEnemy");
    }//BGMCorutine

}//BigEnemyManager
