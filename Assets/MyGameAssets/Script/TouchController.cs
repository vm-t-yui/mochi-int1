using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タッチクラス
/// </summary>
public class TouchController : MonoBehaviour
{
    bool isTouch = false;  //タッチフラグ
	bool isSwipe = false;  //スワイプフラグ

	/// <summary>
	/// 更新処理
	/// </summary>
	void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // タッチ
            if (touch.phase == TouchPhase.Began)
            {
				isTouch = true;
			}
            // スワイプ
            if (touch.deltaPosition.magnitude > 1.5f)
            {
				isSwipe = true;
			}
        }
    }

    /// <summary>
    /// タッチフラグのゲット＋リセット関数
    /// </summary>
    /// <returns>タッチフラグの値</returns>
    public bool GetIsTouch()
    {
        bool returnflg = isTouch;
        isTouch = false;

        return returnflg;
    }

    /// <summary>
    /// スワイプフラグのゲット＋リセット関数
    /// </summary>
    /// <returns>スワイプフラグの値</returns>
    public bool GetIsSwipe()
    {
        bool returnflg = isSwipe;
        isSwipe = false;

        return returnflg;
    }
}