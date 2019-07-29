using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

[CreateAssetMenu(menuName = "Data/RabbitData")]
public sealed class RabbitData : BaseData
{
    [SerializeField]
    GameObject rabbitModel = default;

    [SerializeField]
    [Range(0,100)]
    int spawnRate = 0;

    public GameObject RabbitModel { get { return rabbitModel; } }
    public int       SpawnRate    { get { return spawnRate;    } }
}
