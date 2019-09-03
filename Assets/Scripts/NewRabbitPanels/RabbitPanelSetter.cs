using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ウサギのパネルをセットする
/// </summary>
public class RabbitPanelSetter : MonoBehaviour
{
    // 元のウサギのパネルの親オブジェクト
    [SerializeField]
    Transform sourceRabbitPanelParent = default;

    // 横方向のパネルの数
    [SerializeField]
    int horizontalPanelNum = 0;

    // セットされたパネルのリスト
    List<Transform> setedPanels = new List<Transform>();

    /// <summary>
    /// 開始
    /// </summary>
    void OnEnable()
    {
        // 新しく救出したウサギのリストを取得
        IReadOnlyList<RabbitData> newRescuedRabbits = RabbitPictureBookFlagSwitcher.Inst.NewRescuedRabbits;

        // 現在セット済みのパネルの数
        int currentSetedPanelNum = 0;
        // パネルをセットする行
        int setPanelRowNum = 0;

        foreach(RabbitData rabbit in newRescuedRabbits)
        {
            // ウサギの名前からリザルトのパネルを取得
            Transform panel = sourceRabbitPanelParent.Find(rabbit.Id);
            // 取得したパネルの親オブジェクトを変更してセットする
            panel.SetParent(transform.GetChild(setPanelRowNum));
            // セットしたパネルをオンにする
            panel.gameObject.SetActive(true);
            // セットしたパネルをリストに追加
            setedPanels.Add(panel);

            // セット済みのパネルの数を増やす
            currentSetedPanelNum++;
            
            // 同じ行のパネルが３つを超えようとした場合は次の行に移る
            if (currentSetedPanelNum % horizontalPanelNum == 0)
            {
                setPanelRowNum++;
            }

        }
    }

    /// <summary>
    /// 終了
    /// </summary>
    void OnDisable()
    {
        foreach(Transform panel in setedPanels)
        {
            // セットされたパネルを全てもとの親オブジェクトに戻す
            panel.SetParent(sourceRabbitPanelParent);
            // 戻したパネルをオフにする
            panel.gameObject.SetActive(false);
        }

        // リストを削除する
        setedPanels.Clear();
        // 新しく救出したウサギのリストをクリアする
        RabbitPictureBookFlagSwitcher.Inst.ClearNewRescuedRabbitData();
    }
}
