using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using VMUnityLib;

/// <summary>
/// レアリティ関連の情報を表示するウィンドウ
/// NOTE : 各レアリティとそれに属しているウサギの確立を別ウィンドウで表示しています。
///        確率の確認のためだけなのでコード自体は少し雑です。
/// </summary>
public class RarityViewer : EditorWindow
{
    // ウサギのデータマネージャー
    static public IdentifiedDataManager<RabbitData> RabbitDataManager { get; private set; }
    static IEnumerable<RabbitRarityData> rarityDatas = null;

    // ウサギのレアリティのデータマネージャー
    static public IdentifiedDataManager<RabbitRarityData> RabbitRarityDataManager { get; private set; }
    static IEnumerable<RabbitData> rabbitDatas = null;

    // スクロール位置
    Vector2 scrollPos = Vector2.zero;

    [MenuItem("Window/RarityViewer")]
    /// <summary>
    /// GUIのインスタンス取得
    /// /// </summary>
    private static void Open()
    {
        // ウサギのレアリティを読み込む
        RabbitRarityDataManager = new IdentifiedDataManager<RabbitRarityData>("TowerObjectDatas/Rabbit/RarityDatas");
        RabbitRarityDataManager.LoadData();
        rarityDatas = RabbitRarityDataManager.GetAllData();

        // ウサギを読み込む
        RabbitDataManager = new IdentifiedDataManager<RabbitData>("TowerObjectDatas/Rabbit/RabbitDatas");
        RabbitDataManager.LoadData();
        rabbitDatas = RabbitDataManager.GetAllData();

        GetWindow<RarityViewer>("RarityViewer");
    }

    /// <summary>
    /// GUI全般処理
    /// </summary>
    void OnGUI()
    {
        // スクロール開始位置
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        // レアリティを表示
        ShowRarity();

        // スクロール終了位置
        EditorGUILayout.EndScrollView();
    }

    /// <summary>
    /// ウサギの各レアリティを表示
    /// </summary>
    void ShowRarity()
    {
        // 各確率の合計値
        float totalRate = 0;
        // 各確率の合計値を算出
        foreach (RabbitRarityData rarityData in RabbitRarityDataManager.GetAllData())
        {
            totalRate += rarityData.LotteryRate;
        }

        // 確率のパーセントを表示
        foreach (RabbitRarityData rarityData in rarityDatas)
        {
            // 確率のパーセントを計算
            float percent = (rarityData.LotteryRate / totalRate) * 100;
            GUILayout.BeginHorizontal();
            // 計算結果をログに表示
            EditorGUILayout.LabelField("RabbitRarity : " + rarityData.Id.Substring(("RabbitRarity").Length, 1), EditorStyles.boldLabel);
            EditorGUILayout.LabelField(percent.ToString("f2"), EditorStyles.boldLabel);
            GUILayout.EndHorizontal();

            // レアリティに属しているウサギの確率を表示
            ShowRabbitSpawnRate(rarityData);
            // 一行あける
            EditorGUILayout.LabelField("");
        }
    }

    /// <summary>
    /// ウサギの出現確率を表示
    /// </summary>
    void ShowRabbitSpawnRate(RabbitRarityData rarityData)
    {
        // そのレアリティに属しているウサギのリスト
        List<RabbitData> belongRbbits = new List<RabbitData>();

        // レアリティに属しているウサギを取得する
        foreach(string belongRabbitId in rarityData.RabbitIds)
        {
            foreach (RabbitData rabbitData in rabbitDatas)
            {
                // IDで比較する
                if (belongRabbitId == rabbitData.Id)
                {
                    belongRbbits.Add(rabbitData);
                }
            }
        }

        // 各確率の合計値
        float totalRate = 0;
        // 各確率の合計値を算出
        foreach(RabbitData belongRabbit in belongRbbits)
        {
            totalRate += belongRabbit.SpawnRate;
        }
        
        // 確率のパーセントを表示
        foreach (RabbitData belongRabbit in belongRbbits)
        {
            // 確率のパーセントを計算
            float percent = (belongRabbit.SpawnRate / totalRate) * 100;
            GUILayout.BeginHorizontal();
            // 計算結果をログに表示
            EditorGUILayout.LabelField(belongRabbit.Id);
            EditorGUILayout.LabelField(percent.ToString("f2"));
            GUILayout.EndHorizontal();
        }
    }
}
