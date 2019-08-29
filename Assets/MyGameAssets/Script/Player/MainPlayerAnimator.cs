using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

/// <summary>
/// メインゲーム時のプレイヤーのアニメーション管理クラス
/// </summary>
public class MainPlayerAnimator : SingletonMonoBehaviour<MainPlayerAnimator>
{
    // アニメーションの種類
    public enum AnimKind
    {
        Main,                               // メイン
        Wait,                               // 待機
        RightPunch,                         // 右パンチ
        LeftPunch,                          // 左パンチ
        Rescue,                             // うさぎ救助
        SpecialArts,                        // 最後の大技
        OrangeCatch,                        // ハイスコア時のみかんキャッチ
        Lenght,                             // enumの長さ
    }

    [SerializeField]
    Animator playerAnim = default;          // アニメーター

    [SerializeField]
    Animator orangeAnim = default;          // アニメーター

    [SerializeField]
    SceneChanger sceneChanger = default;    // シーンチェンジャー

    [SerializeField]
    MainCameraAnimator mainCameraAnim = default;          // カメラのアニメーター

    [SerializeField]
    TowerBreakController towerBreak = default;  // タワーを吹っ飛ばすクラス

    /// <summary>
    ///  アニメーション再生
    /// </summary>
    /// <param name="kind">アニメーションの種類</param>
    public void AnimStart(int kind)
    {
        // 種類に応じたアニメーション開始
        switch (kind)
        {
            case (int)AnimKind.Wait: playerAnim.SetTrigger("Wait"); break;
            case (int)AnimKind.RightPunch: playerAnim.SetTrigger("RPunch"); break;
            case (int)AnimKind.LeftPunch: playerAnim.SetTrigger("LPunch"); break;
            case (int)AnimKind.Rescue: playerAnim.SetTrigger("Rescue"); break;
            case (int)AnimKind.SpecialArts: playerAnim.SetTrigger("SpecialArts"); break;
            case (int)AnimKind.OrangeCatch:
                if (GameDataManager.Inst.PlayData.LastScore < ScoreManager.NormalScore)
                {
                    playerAnim.SetTrigger("LowScore");
                    // オレンジの皮落下
                    orangeAnim.SetTrigger("PeelFall");
                }
                {
                    playerAnim.SetTrigger("HighScore");
                    // オレンジ落下
                    orangeAnim.SetTrigger("Fall");
                }

                // NOTE:初回起動処理時はもともと待機モーションに入っており、
                //      トリガーが残ってしまっているのでここでリセットを挟んでいます。
                playerAnim.ResetTrigger("Main"); break;
        }
    }

    /// <summary>
    /// パンチ終了
    /// </summary>
    public void PunchEnd()
    {
        playerAnim.ResetTrigger("RPunch");
        playerAnim.ResetTrigger("LPunch");
    }

    /// <summary>
    /// シーンチェンジ
    /// </summary>
    /// NOTE:メインゲームシーン時のみかんキャッチ後に呼ぶアニメーションイベント用関数です。
    public void ChangeScene()
    {
        // シーン切り替え
        sceneChanger.ChangeScene();

        // プレイ回数をセーブ
        GameDataManager.Inst.PlayData.PlayCount = GameDataManager.Inst.PlayData.PlayCount++;
        JsonDataSaver.Save(GameDataManager.Inst.PlayData);
    }

    /// <summary>
    /// 大技時のカメラアニメーションスタート関数
    /// </summary>
    public void StartSpecialArtsCameraAnim()
    {
        mainCameraAnim.AnimStart((int)MainCameraAnimator.AnimKind.SpecialArts);
    }

    /// <summary>
    /// タワーを全て餅に切り替えるアニメーションイベント用関数
    /// NOTE:必殺技の時にうさぎまで吹き飛ばさないようにもちに切り替えるための処理
    /// </summary>
    public void TowerReplaceRabbitObject()
    {
        towerBreak.ReplaceRabbitObject(false);
    }

    /// <summary>
    /// タワーを吹っ飛ばすアニメーションイベント用関数
    /// </summary>
    public void TowerFly()
    {
        towerBreak.enabled = true;
    }

    /// <summary>
    /// 指定SE再生
    /// </summary>
    public void PlaySe(string id)
    {
        SePlayer.Inst.PlaySe(id);
    }

    /// <summary>
    /// 指定SE再生（ランダムピッチ）
    /// </summary>
    public void PlaySeRandomPitch(string id)
    {
        SePlayer.Inst.PlaySeRandomPitch(id);
    }
}
