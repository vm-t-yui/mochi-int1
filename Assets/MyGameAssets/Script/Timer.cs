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
    FeverTimeController feverTime = default;               // フィーバータイム管理クラス

    [SerializeField]
    float gameTime    = 0;                                 // ゲーム内の秒数
    [SerializeField]
    float plusSeconds = 0;                                 // プラスする秒数

    float startTime = 3;                                   // ゲームスタートまでの秒数

    public float CountTime { get; private set; } = 0;      // 計測用変数

    const float AlertTime = 3.0f;                         // タイムリミット迫り演出開始時間

    bool isAble = false;                                   // 処理許可フラグ

    public bool IsTimeup { get; private set; } = false;    // タイムアップフラグ
    public bool IsStart  { get; private set; } = false;    // ゲームスタートまでのカウントダウンフラグ

    bool isStop = false;                                   // タイマーストップフラグ

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        isAble = false;
    }

    /// <summary>
    /// カウント開始処理
    /// </summary>
    void CountStart()
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
        // Nendの広告のフェードアウトが終わったら処理を許可する
        if (!isAble)
        {
            timer.text = startTime.ToString();

            if (AdManager.Inst.EndFade())
            {
                CountStart();
            }
        }

        // フィーバータイム時の処理
        FeverTimeTimer();

        // 処理が許可されていない or タイムアップ or タイマーがストップしているならしているなら処理を抜ける
        if (!isAble || IsTimeup || isStop) { return; }

        // カウントダウン
        CountTime -= Time.deltaTime;

        // ゲームスタートまでのカウントダウン
        if (!IsStart)
        {
            // 表示
            timer.text = ((int)CountTime + 1).ToString();

            // カウントダウンが終わったらゲーム開始
            FinishCountDown();
        }
        // ゲーム内制限時間カウントダウン
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

            // カウントダウンが終わったらタイムアップ
            FinishCountDown();
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
	/// フィーバータイム時の処理
	/// </summary>
	void FeverTimeTimer()
	{
		// フィーバータイム中はタイマーをストップ、終わればリスタート
		if (feverTime.IsFever && !isStop)
		{
			isStop = true;
            timer.text = "Fever Time !!!";
		}
		else if (!feverTime.IsFever && isStop)
		{
			isStop = false;
            timer.text = CountTime.ToString("f2");
		}
	}

    /// <summary>
    /// それぞれのカウントダウンが終わった時の処理
    /// </summary>
    void FinishCountDown()
    {
        // カウントダウンが終了したらそれぞれの処理へ
        if (CountTime < 0)
        {
            // ゲームスタートのカウントダウンならゲームスタート
            if (!IsStart)
            {
                IsStart = true;
                CountTime = gameTime;
                animator.SetBool("IsCountDown", false);
            }
            // ゲーム内制限時間のカウントダウンならタイムアップ
            else
            {
                timer.text = "Time UP";
                animator.SetBool("IsTimeLimit", false);
                IsTimeup = true;

                // スコアのセーブ
                ScoreManager.Inst.Save();
            }
        }
    }

    /// <summary>
    /// うさぎを助けた時のタイムプラス
    /// </summary>
    public void TimePlus()
    {
        CountTime += plusSeconds;
    }
}
