using UnityEngine;
using VMUnityLib;

/// <summary>
/// メインゲームのサウンド管理クラス
/// </summary>
public class MainSoundManager : MonoBehaviour
{
    [SerializeField]
    BgmPlayer bgmPlayer = default;    // BGM再生クラス

    [SerializeField]
    SePlayer  sePlayer  = default;    // SE再生クラス

    [SerializeField]
    Timer     timer     = default;    // タイマークラス

    bool      isPlayed  = false;      // 再生完了フラグ

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
        if (timer.IsStart && !isPlayed)
        {
            bgmPlayer.PlayBgm(BgmID.Main);

            isPlayed = true;
        }
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    void OnDisable()
    {
        bgmPlayer.StopBgm();
        sePlayer.StopSeAll();
    }
}