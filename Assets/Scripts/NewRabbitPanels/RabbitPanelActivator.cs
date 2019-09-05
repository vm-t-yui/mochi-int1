using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ウサギのパネルの表示を制御する
/// </summary>
public class RabbitPanelActivator : MonoBehaviour
{
    // パネルオブジェクトの親
    [SerializeField]
    Transform panelObjectParent = default;

    /// <summary>
    /// 開始
    /// </summary>
    void OnEnable()
    {
        // 新たに救出したウサギがいれば、一覧のパネルを表示する
        if (RabbitPictureBookFlagSwitcher.Inst.NewRescuedRabbits.Count != 0)
        {
            // パネルのオブジェクトをオンにする
            panelObjectParent.gameObject.SetActive(true);
        }
    }
}
