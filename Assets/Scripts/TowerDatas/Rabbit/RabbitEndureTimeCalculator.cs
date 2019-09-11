using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ウサギの耐久時間の計算を行うクラス
/// </summary>
public class RabbitEndureTimeCalculator : MonoBehaviour
{
    [SerializeField]
    Timer  timer              = default;    // タイマー

    [SerializeField]
    float MaxEnduranceTime = 0f,  // 最大耐久時間
          MinEnduranceTime = 0f;  // 最小耐久時間

    [SerializeField]
    float ReachTime        = 0f;  // 最小にいくまでにかかる時間

    [SerializeField]
    float DivisionNum      = 0;   // 分割数

    float difference       = 0f;  // 最大・最小の差分

    float decTime          = 0f;  // N秒毎にX減らすのNの部分
    float decEnduranceNum  = 0f;  // N秒毎にX減らすのXの部分

    float elapsedTime      = 0f;  // 経過時間
    float prevTime         = 0f;

    bool  isEnd            = false;  // 計算終了フラグ

    public float NowEnduranceTime { get; private set; } = 0f;     // 現在の耐久時間

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        // 最大と最小の減少時間の差分を取得
        difference = MaxEnduranceTime - MinEnduranceTime;
        // ゲージの減少間隔時間の計算
        decTime = ReachTime / DivisionNum;
        // ゲージの減少率の計算
        decEnduranceNum = difference / DivisionNum;
        // 現在の耐久時間を最大時間で初期化
        NowEnduranceTime = MaxEnduranceTime;

        // 現在の減少間隔時間と減少率をログに表示
        Debug.Log(decTime.ToString() + "秒毎に" + decEnduranceNum.ToString() + "減らす");
    }

    /// <summary>
    /// ゲージの減少量の計算
    /// </summary>
    void Update()
    {
        if (timer.IsStart && !timer.isStop)
        {
            // 終了フラグがtrueならここで処理を抜ける
            if (isEnd) { return; }
        
            // 経過時間を計測
            elapsedTime += Time.deltaTime;
        
            // decTime毎に現在の耐久時間を減らす
            if (elapsedTime - prevTime >= decTime)
            {
                prevTime = elapsedTime;
                NowEnduranceTime -= decEnduranceNum;
            }
        
            // 耐久時間が最小まで達したら計算を終了
            if (NowEnduranceTime <= MinEnduranceTime)
            {
                NowEnduranceTime = MinEnduranceTime;
                isEnd = true;
            }
        }
    }
}
