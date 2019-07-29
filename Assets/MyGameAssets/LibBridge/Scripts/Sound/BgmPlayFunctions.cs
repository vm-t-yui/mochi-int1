using UnityEngine;
using VMUnityLib;

/// <summary>
/// BGMの種類ごとに再生を管理するクラス
/// </summary>
public class BgmPlayFunctions : MonoBehaviour
{
    [SerializeField]
    BgmPlayer bgmPlayer = default;    // BGM再生クラス

    /// <summary>
    /// タイトルBGM.
    /// </summary>
    public void PlayTitleBGM()
    {
        bgmPlayer.PlayBgm("TitleBGM");
    }

    /// <summary>
    /// メインゲームBGM.
    /// </summary>
    public void PlayMainBGM()
    {
        bgmPlayer.PlayBgm("MainBGM");
    }

    /// <summary>
    /// リザルトBGM.
    /// </summary>
    public void PlayResultBGM()
    {
        bgmPlayer.PlayBgm("ResultBGM");
    }
}
