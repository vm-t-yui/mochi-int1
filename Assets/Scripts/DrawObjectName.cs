using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawObjectName : MonoBehaviour
{
    [SerializeField]
    Transform mochiSpawnPool = default;

    [SerializeField]
    Transform rabbitSpawnPool = default;

    void Start()
    {
        var objectName = GetComponent<TextMesh>();
        string nameText = transform.parent.parent.name;
        objectName.text = nameText.Replace("(Clone)", "");
    }
}
