using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// タワーが飛ばされる処理を制御
/// </summary>
public class TowerFlyController : MonoBehaviour
{
    // 飛ぶスピード
    [SerializeField]
    float flySpeed = 0;
    
    // 飛ぶ時間
    [SerializeField]
    int flyTime = 0;
    
    // 初期位置
    Vector3 initPos = Vector3.zero;
}