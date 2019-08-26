﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

/// <summary>
/// ウサギ図鑑のフラグを切り替えるクラス
/// </summary>
public class RabbitPictureBookFlagSwitcher : SingletonMonoBehaviour<RabbitPictureBookFlagSwitcher>
{
    // タワーオブジェクトデータ管理クラス
    [SerializeField]
    TowerObjectDataManager towerObjectDataManager = default;

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
        towerObjectDataManager.RabbitDataManager.GetData(rabbitName, out rescuedRabbitData);

        // まだ登録されていなければ、登録を行う
        if (rescuedRabbitNumbers.Find(rabbit => rabbit == rescuedRabbitData.Number) == 0)
        {
            // ウサギの番号をリストに追加
            rescuedRabbitNumbers.Add(rescuedRabbitData.Number);
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
                isReleasedRabbits[rabbitNumber] = true;
            }
        }
    }

    /// <summary>
    /// 終了
    /// </summary>
    void OnDisable()
    {
        // リストに登録されているウサギをもとにフラグを切り替える
        SwitchPictureBookFlag();
        // フラグをもとにJsonにセーブする
        JsonDataSaver.Save(GameDataManager.Inst.PlayData);
        // リストに登録されている救出されたウサギのデータを全削除する
        AllRemoveNumbersList();
    }
}