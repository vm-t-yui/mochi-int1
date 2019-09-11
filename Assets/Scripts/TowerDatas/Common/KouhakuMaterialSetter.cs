using UnityEngine;
using PathologicalGames;

/// <summary>
/// 紅白餅用マテリアルセットクラス
/// </summary>
public class KouhakuMaterialSetter : MonoBehaviour
{
    [SerializeField]
    SpawnPool mochiPool = default;           // モチのプール

    [SerializeField]
    Material[] kouhakuMaterials = default;   // 紅白のマテリアル

    const int CreatedNum = 20;               // 生成しているモチの個数

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        int num = 0;
        foreach (Transform item in mochiPool.transform)
        {
            // 名前に"Kouhaku"が含まれている子のみ設定対象にする
            if (item.ToString().Contains("KouhakuMochi"))
            {
                // 「num % 2 == 0 ? 1 : 0」この式により交互にマテリアルを設定
                item.GetChild(0).GetComponent<Renderer>().material = kouhakuMaterials[num % 2 == 0 ? 1 : 0];
                num++;
            }

            // 設定した個数が生成しているモチの個数に達したら処理を抜ける
            if (num >= CreatedNum)
            {
                break;
            }
        }
    }
}
