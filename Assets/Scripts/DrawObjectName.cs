using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// モチとウサギの種類をテキストで表示するデバッグ用クラス
/// NOTE : デバッグ用なので、ある程度完成した段階で削除します。
/// </summary>
public class DrawObjectName : MonoBehaviour
{
    // モチのスポーンプール
    [SerializeField]
    Transform mochiSpawnPool = default;

    // ウサギのスポーンプール
    [SerializeField]
    Transform rabbitSpawnPool = default;

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // コンポーネントを取得
        var objectName = GetComponent<TextMesh>();
        
        // オブジェクトの名前を取得
        string nameText = transform.parent.parent.name;
        // 全体の名前から"(clone)***"を除いたもとの名前のみを取得
        string sourceName = nameText.Substring(0, nameText.Length - ("(clone)***").Length);
        // 取り出した名前を反映
        objectName.text = sourceName;
    }
}
