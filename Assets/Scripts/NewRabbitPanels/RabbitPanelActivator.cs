using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ウサギのパネルの表示を制御する
/// </summary>
public class RabbitPanelActivator : MonoBehaviour
{
    // パネルオブジェクトの親
    [SerializeField]
    Transform panelObjectParent = default;

    // スコアカウンター
    [SerializeField]
    ScoreCountUpper scoreCountUpper = default;

    // フェードアニメーター
    [SerializeField]
    Animator fadeAnimator = default;

    // 表示可能な状態になってからの待機時間
    [SerializeField]
    int activeWaitTime = 0;
    // 待機時間のカウンタ
    int waitTimeCounter = 0;

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // カウントアップが終了したら、新規ウサギ一覧を表示
        if (scoreCountUpper.IsEnd)
        {
            // カウンターが指定時間を超えたら
            if (waitTimeCounter > activeWaitTime)
            {
                // 新たに救出したウサギがいれば、一覧のパネルを表示する
                if (RabbitPictureBookFlagSwitcher.Inst.NewRescuedRabbits.Count != 0)
                {
                    // アニメーションの再生を開始する
                    fadeAnimator.SetTrigger("FadeIn");
                }
            }
            // カウンター
            waitTimeCounter++;
        }
    }

    /// <summary>
    /// 終了
    /// </summary>
    void OnDisable()
    {
        // カウンターを初期化
        waitTimeCounter = 0; ;
    }
}
