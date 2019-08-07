using UnityEngine;
using VMUnityLib;

/// <summary>
/// タイトルのサウンド管理クラス
/// </summary>
public class TitleSoundManager : MonoBehaviour
{
    [SerializeField]
    BgmPlayer bgmPlayer = default;    // BGM再生クラス

    [SerializeField]
    SePlayer sePlayer   = default;    // SE再生クラス

    bool     isPlayed   = false;      // 再生完了フラグ

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        isPlayed = false;
    }

    /// <summary>
    /// 開始処理
    /// </summary>
    void Update()
    {
        // まだBGMを再生していなければ再生する
        if (!isPlayed)
        {
            bgmPlayer.PlayBgm(BgmID.Title);

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
