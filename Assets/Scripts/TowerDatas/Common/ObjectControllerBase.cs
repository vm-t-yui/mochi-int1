using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タワーオブジェクトのベースクラス
/// </summary>
public abstract class ObjectControllerBase : MonoBehaviour
{
    /// <summary>
    /// プレイヤーからパンチを受けたとき
    /// </summary>
    public abstract void OnPlayerPunched();

    /// <summary>
    /// プレイヤーから助けられた
    /// </summary>
    public abstract void OnPlayerRescued();

    /// <summary>
    /// プレイヤーから最後の大技を受けたとき
    /// </summary>
    public abstract void OnPlayerSpecialArts();
}
