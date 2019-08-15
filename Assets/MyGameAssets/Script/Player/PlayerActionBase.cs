using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのベースクラス
/// </summary>
public abstract class PlayerActionBase : MonoBehaviour
{
    /// <summary>
    /// パンチ
    /// </summary>
    public abstract void OnPunch();

    /// <summary>
    /// うさぎ救助
    /// </summary>
    public abstract void OnRescue();

    /// <summary>
    /// 最後の大技
    /// </summary>
    public abstract void OnSpecialArts();
}
