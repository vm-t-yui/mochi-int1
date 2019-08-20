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

    public float CountTime { get; private set; } = 0;      // 計測用変数

    const float AlertTime = 10.0f;                         // タイムリミット迫り演出開始時間

    bool isAble = false;                                   // 処理許可フラグ

    public bool IsTimeup { get; private set; } = false;    // タイムアップフラグ
    public bool IsStart  { get; private set; } = false;    // ゲームスタートまでのカウントダウンフラグ

     void OnEnable()
    {
        // すぐカウントダウンが始まってしまうため少し遅らせる
        Invoke("_CountStart", 0.5f);
    }

    /// <summary>
    /// カウント開始処理呼び出し関数
    /// NOTE: m.tanaka メインのフェードアウトが終わったら呼ばれるようになってます
    ///       k.oishi  UTweenAlphaの呼びたしが一度しかされないためとりあえずOnEnableで呼ぶようにしました
    /// </summary>
    public void CountStart()
    {
        // すぐカウントダウンが始まってしまうため少し遅らせる
        //Invoke("_CountStart", 0.5f);
    }

    /// <summary>
    /// カウント開始処理
    /// </summary>
    void _CountStart()
    {
        // 処理を許可
        isAble = true;

        // 各フラグをリセット
        IsTimeup = false;
        IsStart = false;

        // ゲーム開始前のカウントをセット
        CountTime = startTime;

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
        CountTime -= Time.deltaTime;

        // ゲームスタートまでのカウントダウン開始
        if (!IsStart)
        {
            // カウントダウンが終わったらゲーム開始
            if (CountTime < 1)
            {
                IsStart = true;
                CountTime = gameTime;
                animator.SetBool("IsCountDown", false);
            }
            // 数え終わってない場合は、数え続ける
            else
            {
                timer.text = ((int)CountTime).ToString();
            }
        }
        // ゲーム開始されたらゲーム内のカウントダウン開始
        else
        {
            // 小数点第2位まで表示
            timer.text = CountTime.ToString("f2");

            // 制限時間が指定時間を下回ったら警告アニメーション再生
            if (CountTime <= AlertTime)
            {
                animator.SetBool("IsTimeLimit", true);
            }
            // 上回っているなら停止
            else
            {
                animator.SetBool("IsTimeLimit", false);
            }

            // 指定の秒数を数え終わったらタイムアップ
            if (CountTime < 0)
            {
                timer.text = "Time UP";

                animator.SetBool("IsTimeLimit", false);

                IsTimeup = true;
                isAble = false;
            }
        }
    }

    /// <summary>
    /// 停止処理
    /// </summary>
    void OnDisable()
    {
        IsTimeup = false;
    }

    /// <summary>
    /// うさぎを助けた時のタイムプラス
    /// </summary>
    public void TimePlus()
    {
        CountTime += plusSeconds;
    }
}
