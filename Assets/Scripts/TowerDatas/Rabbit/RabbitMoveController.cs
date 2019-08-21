using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

/// <summary>
/// ウサギの制御を行う
/// </summary>
public class RabbitMoveController : MoveControllerBase
{
    // オブジェクトのスポーンクラス
    TowerObjectSpawner towerObjectSpawner = default;

    // アニメーターコンポーネント
    [SerializeField]
    Animator animator = default;

    // ウサギのデータ
    [SerializeField]
    RabbitData rabbitData = default;

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // 親オブジェクトにアタッチされているスポナークラスを取得する
        towerObjectSpawner = transform.root.GetComponent<TowerObjectSpawner>();
    }

    // TODO : 以下の関数にそれぞれのアクションに対応したオブジェクトの処理を実装する

    /// <summary>
    /// プレイヤーからパンチを受けたとき
    /// </summary>
    public override void OnPlayerPunched()
    {
        // ウサギが吹っ飛ぶアニメーションを再生
        animator.SetTrigger("RabbitFling");
    }

    /// <summary>
    /// プレイヤーから助けられたとき
    /// </summary>
    public override void OnPlayerRescued()
    {
        // ウサギが救出されるアニメーションを再生
        animator.SetTrigger("RabbitRescued");
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
