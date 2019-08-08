using UnityEngine;
using TMPro;

/// <summary>
/// タイマークラス
/// </summary>
public class Timer : MonoBehaviour
{
    [SerializeField]
    Animator        animator = default;                    // アニメーター
    [SerializeField]
    TextMeshProUGUI timer    = default;                    // タイマー用テキスト

    [SerializeField]
    float startTime   = 0;                                 // ゲームスタートまでの秒数
    [SerializeField]
    float gameTime    = 0;                                 // ゲーム内の秒数
    [SerializeField]
    float plusSeconds = 0;                                 // プラスする秒数

    float countTime = 0;                                   // 計測用変数

    bool isAble = false;                                   // 処理許可フラグ

    public bool IsTimeup { get; private set; } = false;    // タイムアップフラグ
    public bool IsStart  { get; private set; } = false;    // ゲームスタートまでのカウントダウンフラグ

    /// <summary>
    /// カウント開始処理
    /// NOTE: m.tanaka メインのフェードアウトが終わったら呼ばれるようになってます
    /// </summary>
    public void CountStart()
    {
        // すぐカウントダウンが始まってしまうため少し遅らせる
        Invoke("_CountStart", 0.5f);
    }
    void _CountStart()
    {
        // 処理を許可
        isAble = true;

        // 各フラグをリセット
        IsTimeup = false;
        IsStart = false;

        // ゲーム開始前のカウントをセット
        countTime = startTime;

        // カウントダウンアニメーション再生
        animator.SetBool("IsCountDown", true);
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // 処理が許可されていない or タイムアップしているなら処理を抜ける
        if (!isAble || IsTimeup) { return; }

        // カウントダウン
        countTime -= Time.deltaTime;

        // ゲームスタートまでのカウントダウン開始
        if (!IsStart)
        {
            // カウントダウンが終わったらゲーム開始
            if (countTime < 1)
            {
                IsStart = true;
                countTime = gameTime;
                animator.SetBool("IsCountDown", false);
            }
            // 数え終わってない場合は、数え続ける
            else
            {
                timer.text = ((int)countTime).ToString();
            }
        }
        // ゲーム開始されたらゲーム内のカウントダウン開始
        else
        {
            // 小数点第2位まで表示
            timer.text = countTime.ToString("f2");

            if (countTime <= 10.0f)
            {
                animator.SetBool("IsTimeLimit", true);
            }

            // 指定の秒数を数え終わったらタイムアップ
            if (countTime < 0)
            {
                timer.text = "Time UP";

                animator.SetBool("IsTimeLimit", false);

                IsTimeup = true;
                isAble = false;
            }
        }
    }

    /// <summary>
    /// うさぎを助けた時のタイムプラス
    /// </summary>
    public void TimePlus()
    {
        gameTime += plusSeconds;
    }
}
