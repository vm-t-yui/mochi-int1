using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ウサギのアニメーションを制御
/// </summary>
public class RabbitAnimationController : MonoBehaviour
{
    // アニメーター
    [SerializeField]
    Animator animator = default;

    void OnDisable()
    {
        animator.SetTrigger("RescueEnd");
        transform.GetChild(0).gameObject.transform.position = transform.position;
    }
}
