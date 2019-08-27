using UnityEngine;

/// <summary>
/// ギャラリーのコントロールクラス
/// </summary>
public class GalleryController : MonoBehaviour
{
    [SerializeField]
    RectTransform rect  = default;

    [SerializeField]
    float jumpVec         = 3f;          // ジャンプ力
    [SerializeField]
    float Gravity         = 0.25f;       // 重力
    [SerializeField]
    float HighJumpTimeMin = 1.5f,        // 高くジャンプするまでの最大時間
          HighJumpTimeMax = 3f;          // 高くジャンプするまでの最小時間

    float vec             = 0f;          // ジャンプ用変数
    float time            = 0f;          // 時間計測用変数
    float highJumpTime    = 0f;          // 高くジャンプするまでの時間
    float landY           = 0f;          // ギャラリーの地面の高さ

    bool  isStart         = false;       // ジャンプ開始フラグ

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        Invoke("JumpStart", Random.Range(0.1f, 0.3f));
    }
    /// <summary>
    /// ジャンプ開始処理
    /// </summary>
    void JumpStart()
    {
        isStart = true;
        vec = jumpVec;
        highJumpTime = Random.Range(HighJumpTimeMin, HighJumpTimeMax);
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void FixedUpdate()
    {
        if (!isStart) { return; }

        time += Time.deltaTime;

        // ハイジャンプする時間まで達したら
        if (time >= highJumpTime)
        {
            // ジャンプ力を1.5倍で代入
            vec          = jumpVec * 1.5f;

            // 時間のリセットと次のハイジャンプするまでの時間を設定
            time         = 0f;
            highJumpTime = Random.Range(HighJumpTimeMin, HighJumpTimeMax);
        }

        // ジャンプ処理
        Jump();
    }

    /// <summary>
    /// ジャンプ処理
    /// </summary>
    void Jump()
    {
        rect.position += Vector3.up * vec;
        vec -= Gravity;

        if (rect.position.y < -20)
        {
            vec = jumpVec;
        }
    }
}
