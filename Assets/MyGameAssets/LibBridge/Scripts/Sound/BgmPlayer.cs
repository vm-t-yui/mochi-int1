/******************************************************************************/
/*!    \brief  指定BGMを再生する.
*******************************************************************************/

using UnityEngine;
using PathologicalGames;
using System.Collections.Generic;

namespace VMUnityLib
{
    public class BgmPlayer : MonoBehaviour
    {
        SpawnPool bgmPool;    // BGMのプール

        AudioSource spawnedBgm = new AudioSource();    // 再生中のBGM

        /// <summary>
        /// 起動処理.
        /// </summary>
        void Awake()
        {
            bgmPool = GameObject.Find("BgmPool").GetComponent<SpawnPool>();
        }

        /// <summary>
        /// 再生.
        /// </summary>
        public void PlayBgm(string id)
        {
            // 重複再生を防止
            if (spawnedBgm && spawnedBgm.isPlaying)
            {
                spawnedBgm.Stop();
                bgmPool.Despawn(spawnedBgm.transform);
            }

            // 指定した音をスポーン
            Transform spawnedSeTrans = bgmPool.Spawn(id);

            // AudioSourceを取得
            spawnedBgm = spawnedSeTrans.GetComponent<AudioSource>();

            // 再生
            spawnedBgm.Play();
        }

        /// <summary>
        /// ミュート時はボリューム0、非ミュート時は設定データの値を反映.
        /// </summary>
        void LateUpdate()
        {
            // まだBGMが再生されていないなら処理を抜ける
            if (!spawnedBgm) { return; }

            if (GameDataManager.Inst.SettingData.IsBgmMute)
            {
                spawnedBgm.volume = 0;
            }
            else
            {
                spawnedBgm.volume = GameDataManager.Inst.SettingData.BgmVolume;
            }
        }

        /// <summary>
        /// 停止.
        /// </summary>
        public void StopBgm()
        {
            spawnedBgm.Stop();
        }
    }
}