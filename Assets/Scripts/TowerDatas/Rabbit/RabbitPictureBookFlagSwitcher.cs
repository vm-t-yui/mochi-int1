using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

/// <summary>
/// ウサギ図鑑のフラグを切り替えるクラス
/// </summary>
public class RabbitPictureBookFlagSwitcher : Singleton<RabbitPictureBookFlagSwitcher>
{
    // 新しく救出したウサギ
    List<RabbitData> newRescuedRabbits = new List<RabbitData>();
    public IReadOnlyList<RabbitData> NewRescuedRabbits
    {
        get
        {
            return newRescuedRabbits;
        }
    }
    
    // 救出されたウサギの番号
    List<int> rescuedRabbitNumbers = new List<int>();
    public IReadOnlyList<int> RescuedRabbitNumbers
    {
        get
        {
            return rescuedRabbitNumbers;
        }
    }

    /// <summary>
    /// 救出されたウサギを専用のリストに登録
    /// </summary>
    /// <param name="rabbitName"></param>
    public void AddListTheRescuedRabbit(string rabbitName)
    {
        // ウサギの名前からデータを取得
        RabbitData rescuedRabbitData;
        TowerObjectDataManager.Inst.RabbitDataManager.GetData(rabbitName, out rescuedRabbitData);

        // まだ登録されていなければ、登録を行う
        if (rescuedRabbitNumbers.Find(rabbit => rabbit == rescuedRabbitData.Number) == 0)
        {
            // ウサギの番号をリストに追加
            rescuedRabbitNumbers.Add(rescuedRabbitData.Number);

            // うさぎの新規取得フラグをON
            GameDataManager.Inst.PlayData.IsNewReleasedRabbit = true;
        }
    }

    /// <summary>
    /// リストに登録されている番号を全削除
    /// </summary>
    public void AllRemoveNumbersList()
    {
        rescuedRabbitNumbers.Clear();
    }

    /// <summary>
    /// リストに登録されているウサギをもとにフラグを切り替える
    /// </summary>
    public void SwitchPictureBookFlag()
    {
        // 管理クラスのフラグの配列を取得
        bool[] isReleasedRabbits = GameDataManager.Inst.PlayData.IsReleasedRabbit;

        // 登録されているウサギの番号とフラグ配列の同じ番号のフラグを切り替えていく
        foreach (int rabbitNumber in rescuedRabbitNumbers)
        {
            // 図鑑に未だ登録されていないウサギのフラグのみを切り替える
            if (!isReleasedRabbits[rabbitNumber])
            {
                // 救出フラグをオンにする
                isReleasedRabbits[rabbitNumber] = true;
                // Newアイコンの表示フラグをオンにする
                GameDataManager.Inst.PlayData.IsDrawRabbitNewIcon[rabbitNumber] = true;
                // ウサギの番号からデータを取得
                RabbitData rabbitData = TowerObjectDataManager.Inst.GetRabbitDataFromNumber(rabbitNumber);
                // 新しく救出したウサギとしてリストに登録
                newRescuedRabbits.Add(rabbitData);
            }
        }
    }

    /// <summary>
    /// 新しく救出したウサギのデータを削除する
    /// </summary>
    public void ClearNewRescuedRabbitData()
    {
        newRescuedRabbits.Clear();
    }

    /// <summary>
    /// データの終了処理
    /// </summary>
    public void FinalizeData()
    {
        // リストに登録されているウサギをもとにフラグを切り替える
        SwitchPictureBookFlag();
        // フラグをもとにJsonにセーブする
        JsonDataSaver.Save(GameDataManager.Inst.PlayData);
        // リストに登録されている救出されたウサギのデータを全削除する
        AllRemoveNumbersList();
    }
}
