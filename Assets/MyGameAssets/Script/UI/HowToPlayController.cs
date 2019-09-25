using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 遊び方説明ウィンドウクラス
/// </summary>
public class HowToPlayController : MonoBehaviour
{
    [SerializeField]
    TitleSpriteSetter titleSpriteSetter = default;  // タイトルのスプライトセットクラス

    [SerializeField]
    GameObject gameStartButton = default;           // ゲームスタートボタン

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        // 起動時に言語が変わっているかもしれないのでスプライトを更新
        titleSpriteSetter.SetHowToPlay();
    }

    /// <summary>
    /// 初回プレイ時処理
    /// </summary>
    public void FirstGameStart()
    {
        // NOTE:本来なら画面タップでゲームスタートだけど、初回プレイ時のみ、
        //      画面タップすると遊び方説明ウィンドウが自動表示され、それを押すことでゲームがスタートします。
        AdManager.Inst.ShowSceneAdNative();
        gameStartButton.SetActive(false);
    }
}