using UnityEngine;
using VMUnityLib;

/// <summary>
/// メインゲームのサウンド管理クラス
/// </summary>
public class MainSoundManager : MonoBehaviour
{
    [SerializeField]
    Timer               timer         = default;    // タイマークラス

    bool                isPlayed      = false;      // 再生完了フラグ

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        isPlayed = false;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // メインゲームが始まったらBGM再生
        if (timer.IsStart && !isPlayed)
        {
            BgmPlayer.Inst.PlayBgm(BgmID.Main);

            isPlayed = true;
        }
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    void OnDisable()
    {
        BgmPlayer.Inst.StopBgm();
        SePlayer.Inst.StopSeAll();
    }
}