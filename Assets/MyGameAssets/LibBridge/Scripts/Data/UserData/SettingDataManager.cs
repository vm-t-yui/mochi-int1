using UnityEngine;
using UnityEngine.UI;
using VMUnityLib;

/// <summary>
/// ゲームの設定データ管理クラス
/// </summary>
public class SettingDataManager : MonoBehaviour
{
    [SerializeField]
    Slider   BgmVolume      = default,    // BGMのボリューム調整スライダー
             SeVolume       = default;    // SEのボリューム調整スライダー

    [SerializeField]
    Toggle   BgmMute        = default,    // BGMのミュート切替トグル
             SeMute         = default;    // SEのミュート切替トグル

    [SerializeField]
    Dropdown LanguageSelect = default;    // 使用言語切替ドロップダウン

    [SerializeField]
    Text     UseSkin        = default;    // 使用中の餅スキン表示テキスト

    /// <summary>
    /// 起動処理
    /// </summary>
    void Awake()
    {
        // セーブしていた各種設定をUIに反映
        BgmVolume.value      = GameDataManager.Inst.SettingData.BgmVolume;           // BGMボリューム
        SeVolume.value       = GameDataManager.Inst.SettingData.SeVolume;            // SEボリューム
        BgmMute.isOn         = GameDataManager.Inst.SettingData.IsBgmMute;           // BGMミュート
        SeMute.isOn          = GameDataManager.Inst.SettingData.IsSeMute;            // SEミュート
        LanguageSelect.value = (int)GameDataManager.Inst.SettingData.UseLanguage;    // 使用言語
        ChangeUseSkinText(GameDataManager.Inst.SettingData.UseSkin);                 // 使用スキン
    }

    /// <summary>
    /// 設定データセーブ
    /// </summary>
    public void SaveSettingData()
    {
        // 各種設定をデータ管理クラスに反映
        GameDataManager.Inst.SettingData.BgmVolume   = BgmVolume.value;                                   // BGMボリューム
        GameDataManager.Inst.SettingData.SeVolume    = SeVolume.value;                                    // SEボリューム
        GameDataManager.Inst.SettingData.IsBgmMute   = BgmMute.isOn;                                      // BGMミュート
        GameDataManager.Inst.SettingData.IsSeMute    = SeMute.isOn;                                       // SEミュート
        GameDataManager.Inst.SettingData.UseLanguage = (SettingData.LanguageType)LanguageSelect.value;    // 使用言語

        // 設定データをJSON形式でセーブ
        JsonDataSaver.Save(GameDataManager.Inst.SettingData);
    }

    /// <summary>
    /// 餅スキンセット
    /// NOTE: m.tanaka 餅スキン設定ウィンドウのボタンで呼ぶ関数です
    /// </summary>
    /// <param name="skin">セットするスキン番号</param>
    public void SetMochiSkin(int skin)
    {
        // 指定されたスキンにデータを変更
        GameDataManager.Inst.SettingData.UseSkin = (SettingData.SkinType)skin;

        // 表示テキスト変更
        ChangeUseSkinText(GameDataManager.Inst.SettingData.UseSkin);
    }

    /// <summary>
    /// BGMボリュームスライダー動作時の音量設定関数
    /// </summary>
    /// <param name="slider">使用するスライダー</param>
    public void SetBgmVolume(Slider slider)
    {
        GameDataManager.Inst.SettingData.BgmVolume = slider.value;
    }

    /// <summary>
    /// SEボリュームスライダー動作時の音量設定関数
    /// </summary>
    /// <param name="slider">使用するスライダー</param>
    public void SetSeVolume(Slider slider)
    {
        GameDataManager.Inst.SettingData.SeVolume = slider.value;
    }

    /// <summary>
    /// 使用中スキン表示テキスト変更
    /// </summary>
    /// <param name="skinType">餅スキンの種類</param>
    void ChangeUseSkinText(SettingData.SkinType skinType)
    {
        // 受け取った餅スキンによって表示テキスト変更
        // TODO: 仮で作った機能なので要らない場合は削除します。そのまま使えそうならローカライズにも対応させます。
        switch (skinType)
        {
            case SettingData.SkinType.Nomal:
                UseSkin.text = "普通の餅";
                break;
            case SettingData.SkinType.RedWhite:
                UseSkin.text = "紅白餅";
                break;
            case SettingData.SkinType.Yomogi:
                UseSkin.text = "よもぎ餅";
                break;
            case SettingData.SkinType.Strawberry:
                UseSkin.text = "いちご大福";
                break;
            case SettingData.SkinType.Kashiwa:
                UseSkin.text = "柏餅";
                break;
            case SettingData.SkinType.Isobe:
                UseSkin.text = "磯部餅";
                break;
        }
    }
}
