using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;
using VMUnityLib;

/// <summary>
/// ウサギの制御を行う
/// </summary>
public class RabbitMoveController : MoveControllerBase
{
    // オブジェクトのスポーンクラス
    [SerializeField]
    TowerObjectSpawner towerObjectSpawner = default;

    // ウサギのデータ
    RabbitData rabbitData = default;

    // アニメーターコンポーネント
    Animator animator = default;

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // オブジェクトの元の名前
        string sourceName = null;
        // オブジェクトの名前に(Clone)が入っていた場合
        if (name.Contains("(Clone)"))
        {
            // 全体の名前から"(clone)***"を除いたもとの名前のみを取得
            sourceName = name.Substring(0, name.Length - ("(Clone)***").Length);
        }
        // 入っていなければ、そのまま取得
        else
        {
            sourceName = name;
        }

        // オブジェクトの名前（ID）から管理クラスのウサギのデータを取得
        TowerObjectDataManager.Inst.RabbitDataManager.GetData(sourceName, out rabbitData);

        // アタッチされているアニメーターを取得
        animator = GetComponent<Animator>();
    }

    // TODO : 以下の関数にそれぞれのアクションに対応したオブジェクトの処理を実装する

    /// <summary>
    /// プレイヤーからパンチを受けたとき
    /// </summary>
    public override void OnPlayerPunched()
    {
        // ウサギがパンチされた回数をカウントしていく
        GameDataManager.Inst.PlayData.PunchCount++;
        GameDataManager.Inst.PlayData.TotalPunchCount++;
        // ウサギが吹っ飛ぶアニメーションを再生
        animator.SetTrigger("RabbitFling");
        // ウサギがパンチされた時の効果音再生
        SePlayer.Inst.PlaySe(SeID.RabbitPunch);
    }

    /// <summary>
    /// プレイヤーから助けられたとき
    /// </summary>
    public override void OnPlayerRescued()
    {
        // ウサギが助けられた回数をカウント
        GameDataManager.Inst.PlayData.TotalRescueCount++;
        // オブジェクトの名前を取得
        string nameText = transform.name;
        // 全体の名前から"(clone)***"を除いたもとの名前のみを取得
        string sourceName = nameText.Substring(0, nameText.Length - ("(clone)***").Length);
        // 救出されたウサギを専用のリストに登録
        RabbitPictureBookFlagSwitcher.Inst.AddListTheRescuedRabbit(sourceName);
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
    /// ウサギが耐えられなくなったとき
    /// </summary>
    public override void OnCrashed()
    {
        // ウサギが耐えられなくなったときのアニメーションを再生
        animator.SetTrigger("RabbitCrash");
        // ウサギが耐えられなくなったときの効果音再生
        SePlayer.Inst.PlaySe(SeID.RabbitBlowAway);
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
