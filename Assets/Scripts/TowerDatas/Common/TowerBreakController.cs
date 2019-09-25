using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// タワーが飛ばされる処理を制御
/// </summary>
public class TowerBreakController : MonoBehaviour
{
    // スポナークラス
    [SerializeField]
    TowerObjectSpawner towerObjectSpawner = default;

    // オブジェクトのレンダラーリスト
    [SerializeField]
    TowerObjectRendererList towerObjectRendererList = default;

    // 飛ぶスピード
    [SerializeField]
    float objectFlySpeed = 0;

    // 台座のオブジェクト
    [SerializeField]
    GameObject pedestal = default;

    // 全てのオブジェクトの飛ぶ方向
    List<Vector3> objectFlyDirections = new List<Vector3>();

    /// <summary>
    /// 開始
    /// </summary>
    void OnEnable()
    {
        // 各オブジェクトの飛ぶ方向をランダムで決定する
        for (int i = 0; i < towerObjectSpawner.StackedObjects.Count; i++)
        {
            // 飛ぶ方向をランダムに生成
            Vector3 flyDirection = new Vector3(Random.Range(-1.0f,1.0f),
                                               Random.Range(1, 1),
                                               Random.Range(-1.0f, 1.0f));

            // 決定した方向をリストに追加
            objectFlyDirections.Add(flyDirection);

            // 吹っ飛ばした分のモチをスコアに加える
            ScoreManager.Inst.UpdateGetNum();

            // 台座を非表示にする
            pedestal.SetActive(false);
        }
    }

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        for (int i = 0; i < towerObjectSpawner.StackedObjects.Count; i++)
        {
            // タワーオブジェクトを取得
            Transform towerObject = towerObjectSpawner.StackedObjects[i];
            // ランダムで決まった方向にオブジェクトを飛ばす
            towerObject.Translate(objectFlyDirections[i] * objectFlySpeed);
        }
    }
    
    /// <summary>
    /// 終了
    /// </summary>
    void OnDisable()
    {
        // 方向を格納したリストを全削除
        objectFlyDirections.Clear();
        // 吹き飛ばしの処理を止める
        enabled = false;

        // 台座を表示
        pedestal.SetActive(true);
    }

    /// <summary>
    /// ウサギをモチに置き換える
    /// </summary>
    public void ReplaceRabbitObject(bool isCameraOutOnly)
    {
        // 積みあがったオブジェクトのリストを取得
        IReadOnlyList<Transform> stackedObject = towerObjectSpawner.StackedObjects;

        for (int i = 0; i < stackedObject.Count; i++)
        {
            // 全てを置き換える
            if (!isCameraOutOnly)
            {
                // ウサギをモチに置き換える
                if (stackedObject[i].tag == TagName.Rabbit)
                {
                    towerObjectSpawner.ReplaceRabbitToMochi(i);
                }
            }
            // 画面外のウサギのみを置き換える
            else
            {
                // ウサギのレンダラーを取得
                MeshRenderer[] rabbitRenderer = towerObjectRendererList.MeshRenderers[stackedObject[i].name];

                // タワーのウサギで全てのメッシュが画面の範囲外だったら
                if (stackedObject[i].tag == TagName.Rabbit && rabbitRenderer.ToList().FindIndex(renderer => renderer.isVisible) == -1)
                {
                    // ウサギをモチに置き換える
                    towerObjectSpawner.ReplaceRabbitToMochi(i);
                }
            }
        }
    }
}