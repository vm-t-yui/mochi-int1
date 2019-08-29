using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;
using VMUnityLib;

/// <summary>
/// モチの制御を行う
/// </summary>
public class MochiMoveController : MoveControllerBase
{
    // オブジェクトのスポーンクラス
    [SerializeField]
    TowerObjectSpawner towerObjectSpawner = default;
    
    // パーティクル再生クラス
    [SerializeField]
    TowerObjectParticlePlayer particlePlayer = default;

    // NOTE : モチのほうにはクラッシュの処理はなし（エラー除け）
    public override void OnCrashed() { }

    /// <summary>
    /// プレイヤーからパンチを受けたとき
    /// </summary>
    public override void OnPlayerPunched()
    {
        // パンチによるモチの破壊数を更新する
        ScoreManager.Inst.UpdateGetNum();
        // 破壊パーティクル再生
        particlePlayer.Play("MochiBreak", transform.position, transform.rotation);

        // モチの終了処理
        OnControlFinished();

        // モチ破壊効果音再生
        SePlayer.Inst.PlaySeRandomPitch(SeID.MochiBreak);
    }

    /// <summary>
    /// プレイヤーから助けられたとき
    /// </summary>
    public override void OnPlayerRescued()
    {
        // モチの終了処理
        OnControlFinished();
    }

    /// <summary>
    /// プレイヤーの最後の大技をうけたとき
    /// </summary>
    public override void OnPlayerSpecialArts()
    {

    }

    /// <summary>
    /// オブジェクトのアクションが終了したときに呼ぶコールバック
    /// </summary>
    public void OnControlFinished()
    {
        // オブジェクト自信をデスポーンする
        towerObjectSpawner.Despawn(transform);
    }
}
