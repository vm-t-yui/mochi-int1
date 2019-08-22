using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerActionEventTest : MonoBehaviour
{
    [SerializeField]
    UnityEvent punched = default;

    [SerializeField]
    UnityEvent rescued = default;

    [SerializeField]
    UnityEvent specialArts = default;

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            punched.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            rescued.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            specialArts.Invoke();
        }
    }
}
