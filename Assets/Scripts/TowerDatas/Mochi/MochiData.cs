using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

/// <summary>
/// モチ単体のデータを保持するクラス
/// </summary>
[CreateAssetMenu(menuName = "Data/MochiData")]
public sealed class MochiData : BaseData
{
    // モチのオブジェクトPrefab
    [SerializeField]
    GameObject mochiObject = default;

    // モチのスキン取得に必要なスコア値
    [SerializeField] uint getSkinScore = default;

    public GameObject MochiObject  { get { return mochiObject;  }}
    public uint       GetSkinScore { get { return getSkinScore; }}
}
