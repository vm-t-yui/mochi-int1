using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// タワーが飛ばされる処理を制御
/// TODO : タワー全体の不具合を修正した影響で、こちらの処理でエラーが出始めたので一度処理を全て削除しました。
///        次回、修正していきたいと思います。
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