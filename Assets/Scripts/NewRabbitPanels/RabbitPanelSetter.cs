using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

/// <summary>
/// ウサギのパネルをセットする
/// </summary>
public class RabbitPanelSetter : MonoBehaviour
{
    // 元のウサギのパネルの親オブジェクト
    [SerializeField]
    Transform sourceRabbitPanelParent = default;

    // パネルをセットするオブジェクトの親
    [SerializeField]
    Transform setedRabbitPanelParent = default;

    // HorizontalLayoutのプール
    [SerializeField]
    SpawnPool horizontalLayoutPool = default;

    // HorizontalLayoutのプレハブ名
    const string HorizontalLayoutName = "RowRabbitPanels";

    // 横方向のパネルの数
    [SerializeField]
    int horizontalPanelNum = 0;

    // スポーンしたHorizontalLayoutのリスト
    List<Transform> spawnedLayouts = new List<Transform>();

    // セットされたパネルのリスト
    List<Transform> setedPanels = new List<Transform>();

    /// <summary>
    /// 開始
    /// </summary>
    void OnEnable()
    {
        // 新しく救出したウサギのリストを取得
        IReadOnlyList<RabbitData> newRescuedRabbits = RabbitPictureBookFlagSwitcher.Inst.NewRescuedRabbits;

        // NULLじゃなければ以下の処理を行う
        if (newRescuedRabbits == null) { return; }

        // パネルをセットする行
        int setPanelRowNum = 0;

        Transform spawnedLayout;
        // （新しく救出したウサギの数 / 横方向のパネルの数）個のHorizontalLayoutをスポーン
        for (int i = 0; i < Mathf.CeilToInt((float)newRescuedRabbits.Count / horizontalPanelNum); i++)
        {
            spawnedLayout = horizontalLayoutPool.Spawn(HorizontalLayoutName);
            spawnedLayouts.Add(spawnedLayout);
            spawnedLayout.SetParent(setedRabbitPanelParent);
        }

        foreach(RabbitData rabbit in newRescuedRabbits)
        {
            // ウサギの名前からリザルトのパネルを取得
            Transform panel = sourceRabbitPanelParent.Find(rabbit.Id);
            // 取得したパネルの親オブジェクトを変更してセットする
            panel.SetParent(setedRabbitPanelParent.GetChild(setPanelRowNum));
            // セットしたパネルをオンにする
            panel.gameObject.SetActive(true);
            // セットしたパネルをリストに追加
            setedPanels.Add(panel);
            
            // 同じ行のパネルが３つを超えようとした場合は次の行に移る
            if (setedRabbitPanelParent.GetChild(setPanelRowNum).childCount >= horizontalPanelNum)
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

        foreach (Transform layout in spawnedLayouts)
        {
            // HorizontalLayoutの親をプールに戻す
            layout.SetParent(horizontalLayoutPool.transform);

            // デスポーンする
            horizontalLayoutPool.Despawn(layout);
        }

        // リストを削除する
        setedPanels.Clear();
        // 新しく救出したウサギのリストをクリアする
        RabbitPictureBookFlagSwitcher.Inst.ClearNewRescuedRabbitData();
    }
}
