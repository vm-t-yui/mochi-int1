/******************************************************************************/
/*!    \brief  指定BGMを再生する.
*******************************************************************************/

using System.Collections;
using UnityEngine;
using PathologicalGames;

namespace VMUnityLib
{
    public class BgmPlayer : SingletonMonoBehaviour<BgmPlayer>
    {
        [SerializeField]
        SpawnPool bgmPool;                             // BGMのプール

        AudioSource spawnedBgm = new AudioSource();    // 再生中のBGM
        AudioSource pauseBGM   = new AudioSource();    // 一時停止中のBGM

        bool isResult       = false;                   // リザルトフラグ
        bool isPlayedResult = false;                   // リザルトBGM再生完了フラグ

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

            // ミュート時はボリュームを０に、そうでなければ設定データのBGMボリュームを設定
            if (GameDataManager.Inst.SettingData.IsBgmMute)
            {
                spawnedBgm.volume = 0;
            }
            else
            {
                spawnedBgm.volume = GameDataManager.Inst.SettingData.BgmVolume;
            }

            // 再生
            spawnedBgm.Play();
        }

        /// <summary>
        /// 再生（リザルト用）.
        /// </summary>
        public void PlayResultBgm(string id)
        {
            isResult = true;

            PlayBgm(id);
        }

        /// <summary>
        /// 一時停止
        /// </summary>
        public void PauseBgm()
        {
            if (!spawnedBgm.isPlaying) { return; }

            spawnedBgm.Pause();

            pauseBGM = spawnedBgm;
        }

        /// <summary>
        /// 一時停止していたBGMを再生
        /// </summary>
        public void ReStartBgm()
        {
            pauseBGM.Play();

            spawnedBgm = pauseBGM;
        }

        /// <summary>
        /// フェードイン
        /// </summary>
        /// <param name="fadeTime">フェードにかかる時間</param>
        public void FadeIn(float fadeTime)
        {
            // ミュート時は処理を抜ける
            if (GameDataManager.Inst.SettingData.IsBgmMute) { return; }

            StartCoroutine(_FadeIn(fadeTime));
        }
        IEnumerator _FadeIn(float fadeTime)
        {
            // 設定データのBGMボリュームに到達するまでループ
            while (spawnedBgm.volume < GameDataManager.Inst.SettingData.BgmVolume)
            {
                spawnedBgm.volume += GameDataManager.Inst.SettingData.BgmVolume * (Time.deltaTime / fadeTime);

                yield return null;
            }

            // 設定データの値を超えてしまっていた時のために代入処理
            spawnedBgm.volume = GameDataManager.Inst.SettingData.BgmVolume;
        }

        /// <summary>
        /// フェードアウト
        /// </summary>
        /// <param name="fadeTime">フェードにかかる時間</param>
        public void FadeOut(float fadeTime)
        {
            StartCoroutine(_FadeOut(fadeTime));
        }
        IEnumerator _FadeOut(float fadeTime)
        {
            // ０に到達するまでループ
            while (spawnedBgm.volume > 0)
            {
                spawnedBgm.volume -= GameDataManager.Inst.SettingData.BgmVolume * (Time.deltaTime / fadeTime);

                yield return null;
            }

            // ０を下回ってしまっていた時のために代入処理
            spawnedBgm.volume = 0;

            // BGMを停止
            StopBgm();
        }

        /// <summary>
        /// BGM切り替え（フィーバータイム用）
        /// </summary>
        /// <param name="id">切り替えるBGMのID</param>
        /// <param name="fadeTime">フェードにかかる時間</param>
        public void ChangeBgm(string id, float fadeTime)
        {
            StartCoroutine(_ChangeBgm(id, fadeTime));
        }
        IEnumerator _ChangeBgm(string id, float fadeTime)
        {
            // ０に到達するまでループ
            while (spawnedBgm.volume > 0)
            {
                spawnedBgm.volume -= GameDataManager.Inst.SettingData.BgmVolume * (Time.deltaTime / fadeTime);

                yield return null;
            }

            // ０を下回ってしまっていた時のために代入処理
            spawnedBgm.volume = 0;

            // 再生中のBGMを一時停止
            PauseBgm();

            // 指定されたBGMを再生
            PlayBgm(id);

            // フェードイン開始
            FadeIn(fadeTime);
        }

        /// <summary>
        /// 元のBGMに戻す（フィーバータイム用）
        /// </summary>
        /// <param name="fadeTime">フェードにかかる時間</param>
        public void ReturnBgm(float fadeTime)
        {
            StartCoroutine(_ReturnBgm(fadeTime));
        }
        IEnumerator _ReturnBgm(float fadeTime)
        {
            // ０に到達するまでループ
            while (spawnedBgm.volume > 0)
            {
                spawnedBgm.volume -= GameDataManager.Inst.SettingData.BgmVolume * (Time.deltaTime / fadeTime);

                yield return null;
            }

            // ０を下回ってしまっていた時のために代入処理
            spawnedBgm.volume = 0;

            // BGMを停止
            StopBgm();

            // 一時停止していたBGMを再生
            ReStartBgm();

            // フェードイン開始
            FadeIn(fadeTime);
        }

        /// <summary>
        /// 停止.
        /// </summary>
        public void StopBgm()
        {
            isResult = false;

            // 再生中でなければ処理を抜ける
            if (!spawnedBgm.isPlaying) { return; }

            spawnedBgm.Stop();

            bgmPool.Despawn(spawnedBgm.transform);
        }

        /// <summary>
        /// ミュート時はボリューム0、非ミュート時は設定データの値を反映.
        /// </summary>
        void LateUpdate()
        {
            // まだBGMが再生されていないなら処理を抜ける
            if (!spawnedBgm) { return; }

            // リザルト時
            if (isResult)
            {
                // ジングルが終了したら
                if (!spawnedBgm.isPlaying)
                {
                    StopBgm();

                    // リザルトBGMを再生
                    PlayBgm(BgmID.Result);
                }
            }
        }
    }
}