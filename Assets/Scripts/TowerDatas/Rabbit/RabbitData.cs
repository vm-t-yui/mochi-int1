using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

/// <summary>
/// ウサギ単体のデータを保持するクラス
/// </summary>
[CreateAssetMenu(menuName = "Data/RabbitData")]
public sealed class RabbitData : BaseData
{
    // ウサギの番号
    [SerializeField]
    int number = 0;

    // ウサギの出現率
    [SerializeField]
    [Range(0,100)]
    float spawnRate = 0;

    public int        Number { get { return number; } }
    public float      SpawnRate    { get { return spawnRate;    } }
}
