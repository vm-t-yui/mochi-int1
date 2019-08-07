using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// モチの制御を行う
/// </summary>
public class MochiController : ObjectControllerBase
{
    // オブジェクトのスポーンクラス
    [SerializeField]
    TowerObjectSpawner towerObjectSpawner = default;

    // TODO : 以下の関数にそれぞれのアクションに対応したオブジェクトの処理を実装する

    /// <summary>
    /// プレイヤーからパンチを受けたとき
    /// </summary>
    public override void OnPlayerPunched()
    {
    }

    /// <summary>
    /// プレイヤーから助けられたとき
    /// </summary>
    public override void OnPlayerRescued()
    {

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
        towerObjectSpawner.Despawn(this.transform);
    }
}
