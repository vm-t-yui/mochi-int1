/******************************************************************************/
/*!    \brief  指定SEを再生する.
*******************************************************************************/

using UnityEngine;
using PathologicalGames;
using System.Collections.Generic;

namespace VMUnityLib
{
    public sealed class SePlayer : MonoBehaviour
    {
        SpawnPool sePool;    // プールマネージャー

        List<AudioSource> endWatchSeList = new List<AudioSource>();    // 再生終了チェックリスト

        /// <summary>
        /// 起動処理.
        /// </summary>
        void Awake()
        {
            // SEのプールをセット
            sePool = GameObject.Find("SePool").GetComponent<SpawnPool>();
        }

        /// <summary>
        /// 再生.
        /// </summary>
        public void PlaySe(string id, float volume = 1, float randomPitchBand = 0)
        {
            // 指定した音をスポーン
            Transform spawnedSeTrans = sePool.Spawn(id);

            // AudioSourceを取得
            AudioSource spawnedSe = spawnedSeTrans.GetComponent<AudioSource>();

            // 第3引数が0でない場合はランダムなピッチを設定
            if (randomPitchBand > 0)
            {
                spawnedSe.pitch += Random.Range(-randomPitchBand * 0.5f, randomPitchBand * 0.5f);
            }

            // ボリュームを設定し、再生
            spawnedSe.volume = volume;
            spawnedSe.Play();

            // 再生終了チェックリストに追加
            endWatchSeList.Add(spawnedSe);
        }

        /// <summary>
        /// 再生終了したものをすべて削除する.
        /// </summary>
        void LateUpdate()
        {
            // 再生が終了しているすべての音を取得
            List<AudioSource> removeSeList = endWatchSeList.FindAll(se => !se.isPlaying);

            // 取得したすべての音をデスポーン
            foreach (var item in removeSeList)
            {
                sePool.Despawn(item.transform);

                // 再生終了チェックリストから削除
                endWatchSeList.Remove(item);
            }
        }

        /// <summary>
        /// 全ての音を停止させる.
        /// </summary>
        public void StopSeAll()
        {
            // 再生中のすべての音を取得
            List<AudioSource> removeSeList = endWatchSeList.FindAll(se => se.isPlaying);

            // 取得したすべての音を停止
            foreach (var item in removeSeList)
            {
                item.Stop();
            }
        }
    }
}