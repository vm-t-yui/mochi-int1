using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 図鑑の表示切替
/// </summary>
public class PictureBookController : MonoBehaviour
{
    // すべての説明ウィンドウ
    [SerializeField]
    GameObject[] descriptions = new GameObject[PlayData.AllRabbitNum];

    // 非解放用の説明ウィンドウ
    [SerializeField]
    GameObject notReleaseDescription = default;

    // スクロールビューのボタン
    [SerializeField]
    Transform rabbitButtons = default;

    const int NotReleaseNum = -1;    // 非解放用の説明ウィンドウ番号
    int       nowOpenNum    = 0;     // 表示中の説明ウィンドウ番号

    bool[]    isReleasedRabbit;      // うさぎの解放フラグ

    [SerializeField]
    bool IsDebug = false;            // デバッグ用フラグ（trueにすれば全部の説明ウィンドウを開けるようになります）

    /// <summary>
    /// 起動処理.
    /// </summary>
    void OnEnable()
    {
        // うさぎの解放フラグを取得
        isReleasedRabbit = GameDataManager.Inst.PlayData.IsReleasedRabbit;

        // 最初のうさぎが解放済みなら表示させる
        if (isReleasedRabbit[0])
        {
            OpenDescription(0);
            nowOpenNum = 0;
        }
        // 開放されてないなら非解放用の説明を表示
        else
        {
            notReleaseDescription.SetActive(true);
            nowOpenNum = NotReleaseNum;
        }

        // 解放状況をボタンの画像に反映
        int i = 0;
        foreach(Transform item in rabbitButtons)
        {
            // 解放済みなら1つ目の子を表示（０が解放済み、１が非解放）
            if (isReleasedRabbit[i])
            {
                // Newアイコンが表示フラグがOFFなら1つ目の子の子(Newアイコン)を消す
                if (!GameDataManager.Inst.PlayData.IsDrawRabbitNewIcon[i])
                {
                    item.GetChild(0).GetChild(0).gameObject.SetActive(false);
                }

                item.GetChild(0).gameObject.SetActive(true);
                item.GetChild(1).gameObject.SetActive(false);
            }
            // 解放していないなら2つ目の子を表示
            else
            {
                item.GetChild(0).gameObject.SetActive(false);
                item.GetChild(1).gameObject.SetActive(true);
            }

            i++;
        }
    }

    /// <summary>
    /// 指定した説明ウィンドウを表示する
    /// NOTO: m.tanaka 図鑑のボタンで呼びます
    /// </summary>
    public void OpenDescription(int num)
    {
        // 指定した番号のうさぎが解放済みの場合
        if (isReleasedRabbit[num] || IsDebug)
        {
            // 非解放用の説明を非表示に
            notReleaseDescription.SetActive(false);

            // 非表示中の説明を非表示に
            if (nowOpenNum != NotReleaseNum)
            {
                descriptions[nowOpenNum].SetActive(false);
            }

            // 指定された説明を表示
            descriptions[num].SetActive(true);

            // Newアイコンが表示フラグをオフにして非表示にする
            GameDataManager.Inst.PlayData.IsDrawRabbitNewIcon[num] = false;
            rabbitButtons.GetChild(num).GetChild(0).GetChild(0).gameObject.SetActive(false);

            // 表示中の説明番号を更新
            nowOpenNum = num;
        }
        // 解放されていない場合
        else
        {
            // 非解放用の説明以外が表示されているなら
            if (nowOpenNum != NotReleaseNum)
            {
                // 表示中の説明を非表示に
                descriptions[nowOpenNum].SetActive(false);

                // 非解放用の説明を表示
                notReleaseDescription.SetActive(true);

                // 表示中の説明番号を更新
                nowOpenNum = NotReleaseNum;
            }
        }
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    void OnDisable()
    {
        // 非解放用の説明が表示されている場合
        if (nowOpenNum == NotReleaseNum)
        {
            notReleaseDescription.SetActive(false);
        }
        // それ以外の場合
        else
        {
            descriptions[nowOpenNum].SetActive(false);
        }
    }
}
