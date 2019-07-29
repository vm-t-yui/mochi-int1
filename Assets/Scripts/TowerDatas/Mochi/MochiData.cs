using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

[CreateAssetMenu(menuName = "Data/MochiData")]
public sealed class MochiData : BaseData
{
    [SerializeField]
    GameObject mochiObject = default;

    [SerializeField] uint getSkinScore = default;

    public GameObject MochiObject  { get { return mochiObject;  }}
    public uint       GetSkinScore { get { return getSkinScore; }}
}
