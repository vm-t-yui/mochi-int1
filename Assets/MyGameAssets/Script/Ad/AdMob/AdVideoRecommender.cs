using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

/// <summary>
/// 動画広告勧誘クラス
/// </summary>
public class AdVideoRecommender : MonoBehaviour
{
    [SerializeField]
    AdRewardVideoController adMobVideo = default;             //　AdMob動画リワード広告クラス

    //[SerializeField]
    //UnityAdsRewardController unityAdsVideo = default;         // UnityAds動画リワード広告クラス

    [SerializeField]
    GameObject recommendWindow = default;                     // 勧誘用カンバス

    public bool IsRecommend { get; private set; } = false;    // 勧誘済みフラグ
    public bool isEnd = false;                                // 処理終了フラグ

    const int RecommendInterval = 5;                          // 勧誘を行うプレイ回数間隔

    /// <summary>
    /// 初期化
    /// </summary>
    public void Init()
    {
        // AdMob動画リワード広告を生成を生成
        adMobVideo.RequestRewardVideo();
    }

    /// <summary>
    /// AdMobを使うかどうか
    /// </summary>
    /// <returns></returns>
    bool IsUseAdMob(int num)
    {
        // 広告表示回数が偶数ならAdMob
        if (num / RecommendInterval % 2 == 0)
        {
            return true;
        }
        // 奇数ならUnityAds
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 動画広告再生
    /// </summary>
    public void PlayAdVideo()
    {
        // AdMob再生
        if (IsUseAdMob(GameDataManager.Inst.PlayData.PlayCount))
        {
            adMobVideo.Play();
        }
        // UnityAds再生
        else
        {
            //unityAdsVideo.Play();
        }
    }

    /// <summary>
    /// 動画広告勧誘
    /// </summary>
    public void Recommend()
    {
        // 専用カンバスを表示
        recommendWindow.SetActive(true);

        // 勧誘済みにする
        IsRecommend = true;
    }

    /// <summary>
    /// 動画広告勧誘ウィンドウ削除
    /// </summary>
    public void Cancel()
    {
        recommendWindow.SetActive(false);
    }

    /// <summary>
    /// 動画終了待ち処理
    /// </summary>
    public void WaitTermination()
    {
        // 勧誘、終了していなかったら処理を抜ける
        if (!IsRecommend || !adMobVideo.IsClosed) { return; }

        // 動画広告をスキップしたらスキップフラグを立てる
        if (adMobVideo.IsSkipped)// || unityAdsVideo.IsSkipped)
        {
            // リワード無し
            GameDataManager.Inst.PlayData.IsReward = false;
        }

        // 動画広告を閉じたら処理終了
        if (adMobVideo.IsCompleted)// || unityAdsVideo.IsFinished)
        {
            // リワードあり
            GameDataManager.Inst.PlayData.IsReward = true;
            isEnd = true;
        }

        // リワードフラグをセーブして次の広告を生成
        JsonDataSaver.Save(GameDataManager.Inst.PlayData);
    }

    /// <summary>
    /// 動画広告再生終了検知
    /// </summary>
    /// <returns>動画広告再生終了フラグ</returns>
    public bool EndAdVideo()
    {
        bool returnflg = false;

        if(isEnd)
        {
            returnflg = isEnd;
            isEnd = false;
        }

        return returnflg;
    }
}