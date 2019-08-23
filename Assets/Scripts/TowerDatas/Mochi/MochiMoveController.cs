﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

/// <summary>
/// モチの制御を行う
/// </summary>
public class MochiMoveController : MoveControllerBase
{
    // オブジェクトのスポーンクラス
    TowerObjectSpawner towerObjectSpawner = default;

    // TODO : 以下の関数にそれぞれのアクションに対応したオブジェクトの処理を実装する

    // NOTE : モチのほうにはクラッシュの処理はなし（エラー除け）
    public override void OnCrashed() { }

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // 親オブジェクトにアタッチされているスポナークラスを取得する
        towerObjectSpawner = transform.parent.parent.GetComponent<TowerObjectSpawner>();
    }

    /// <summary>
    /// プレイヤーからパンチを受けたとき
    /// </summary>
    public override void OnPlayerPunched()
    {
        // パンチによるモチの破壊数を更新する
        ScoreManager.Inst.UpdateGetNum();
        // モチの終了処理
        OnControlFinished();
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
