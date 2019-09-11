using VMUnityLib;

/// <summary>
/// 実績解除クラス
/// </summary>
public class AchievementReleaser : Singleton<AchievementReleaser>
{
    // 実績の解除条件
    static bool[] releaseCondisions = new bool[PlayData.AllAchievementNum];

    /// <summary>
    /// 各実績を解除
    /// </summary>
    public static void ReleaseAchievement()
    {
        // プレイデータのインスタンスを取得
        PlayData playData = GameDataManager.Inst.PlayData;

        // 各実績の解除条件を設定
        releaseCondisions[0] = !playData.IsReleasedAchieve[0] && playData.PlayCount > 0;
        releaseCondisions[1] = !playData.IsReleasedAchieve[1] && playData.TotalRescueCount > 500;
        releaseCondisions[2] = !playData.IsReleasedAchieve[2] && playData.TotalScore > 5000;
        releaseCondisions[3] = !playData.IsReleasedAchieve[3] && playData.RabbitComplete();

        for (int i = 0; i < PlayData.AllAchievementNum; i++)
        {
            // 解除条件を満たしていれば解除する
            if (releaseCondisions[i])
            {
//                GameServiceUtil.ReportProgress(i);
                // 実績解除状況を更新
                playData.IsReleasedAchieve[i] = true;
            }
        }

        // セーブ
        JsonDataSaver.Save(GameDataManager.Inst.PlayData);
    }
}