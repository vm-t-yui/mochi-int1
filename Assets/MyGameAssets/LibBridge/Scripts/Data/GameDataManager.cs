/******************************************************************************/
/*!    \brief  ゲームパラメータのマネージャ.
*******************************************************************************/

using UnityEngine;
using VMUnityLib;
using System.Collections;
using System.Collections.Generic;

public class GameDataManager : Singleton<GameDataManager>
{
    public UserData    UserData    { get; set; }
    public PlayData    PlayData    { get; set; }            // プレイデータ
    public SettingData SettingData { get; set; }            // 設定データ
    public ResultKindData resultKindData { get; set; }      // リザルトの分岐データ

    public IdentifiedDataManager<EffectData> EffectDataManager { get; set; }
    public IdentifiedDataManager<VoiceData> VoiceDataManager { get; set; }

    public GameDataManager()
    {

    }

    /// <summary>
    /// 不変データを読み込む.
    /// </summary>
    public void LoadStaticData()
    {
        EffectDataManager = new IdentifiedDataManager<EffectData>("Data/EffectData");
        VoiceDataManager = new IdentifiedDataManager<VoiceData>("Data/VoiceData");
        EffectDataManager.LoadData();
        VoiceDataManager.LoadData();
    }

    /// <summary>
    /// デフォルトデータを読み込む.
    /// </summary>
    public void LoadDefaultData()
    {
        /*
        // TODO:通信実装.
        UserData = GetDummyData();

        // TODO:正しいものに.
        List<string> skillSlotIdList = new List<string>();
        UnitData friendUnit = new UnitData(GetKituneArcheTypeDummy(5), 0, 12, 0, UnitKind.PLAYER, skillSlotIdList);

        UserPublicData friendPublicData = new UserPublicData(
            "friend",
            "ミスターフレンド",
            friendUnit,
            100
            );
        BattleInitializer.Inst.SetSortieInfo(UserData.GetPartyData(0), friendPublicData);
        */

        PlayData = new PlayData();
        SettingData = new SettingData();
        resultKindData = new ResultKindData();

        // 各セーブデータがあればロードする
        PlayData = JsonDataSaver.FileExists(PlayData) ? JsonDataSaver.Load(PlayData) : PlayData;
        SettingData = JsonDataSaver.FileExists(SettingData) ? JsonDataSaver.Load(SettingData) : SettingData;
        resultKindData = JsonDataSaver.FileExists(resultKindData) ? JsonDataSaver.Load(resultKindData) : resultKindData;
    }

    /// <summary>
    /// ダミーデータ作成.
    /// </summary>
    /// <returns></returns>
    UserData GetDummyData()
    {
        UserData ret = new UserData(new UserPublicData("test", "test user"));
        return ret;
    }
}
