using UnityEngine;
using UnityEngine.UI;
using VMUnityLib;
using TMPro;
using I2.Loc;

/// <summary>
/// ゲームの設定データ管理クラス
/// </summary>
public class SettingDataManager : MonoBehaviour
{
    [SerializeField]
    Slider   BgmVolume          = default,    // BGMのボリューム調整スライダー
             SeVolume           = default;    // SEのボリューム調整スライダー

    [SerializeField]
    Toggle   BgmMute            = default,    // BGMのミュート切替トグル
             SeMute             = default;    // SEのミュート切替トグル

    [SerializeField]
    TMP_Dropdown LanguageSelect = default;    // 使用言語切替ドロップダウン

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

    /// NOTE: m.tanaka 以下4つの関数はスライダー等のValue Changedで呼んでください

    /// <summary>
    /// BGMボリュームスライダー動作時の音量設定関数
    /// </summary>
    /// <param name="slider">使用するスライダー</param>
    public void SetBgmVolume(Slider slider)
    {
        GameDataManager.Inst.SettingData.BgmVolume = slider.value;
    }
    /// <summary>
    /// BGMミュートトグル変更時のミュートフラグ設定関数
    /// </summary>
    /// <param name="toggle"></param>
    public void SetBgmMute(Toggle toggle)
    {
        GameDataManager.Inst.SettingData.IsBgmMute = toggle.isOn;
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
    /// SEミュートトグル変更時のミュートフラグ設定関数
    /// </summary>
    /// <param name="toggle"></param>
    public void SetSeMute(Toggle toggle)
    {
        GameDataManager.Inst.SettingData.IsSeMute = toggle.isOn;
    }

    /// <summary>
    /// 使用言語の切り替え
    /// NOTO: m.tanaka ドロップダウンの値変更時に呼んでます
    /// </summary>
    public void ChangeLanguage()
    {
        // 選択されたドロップダウンの番号に応じて言語を切り替える
        switch (LanguageSelect.value)
        {
            // 日本語
            case (int)SettingData.LanguageType.Japanese:
                LocalizationManager.CurrentLanguage = SettingData.LanguageType.Japanese.ToString();
                break;
            // 英語
            case (int)SettingData.LanguageType.English:
                LocalizationManager.CurrentLanguage = SettingData.LanguageType.English.ToString();
                break;
            // ドイツ語
            case (int)SettingData.LanguageType.German:
                LocalizationManager.CurrentLanguage = SettingData.LanguageType.German.ToString();
                break;
            // イタリア語
            case (int)SettingData.LanguageType.Italian:
                LocalizationManager.CurrentLanguage = SettingData.LanguageType.Italian.ToString();
                break;
            // フランス語
            case (int)SettingData.LanguageType.French:
                LocalizationManager.CurrentLanguage = SettingData.LanguageType.French.ToString();
                break;
            // 中国語
            case (int)SettingData.LanguageType.Chinese:
                LocalizationManager.CurrentLanguage = SettingData.LanguageType.Chinese.ToString();
                break;
            // スペイン語
            case (int)SettingData.LanguageType.Spanish:
                LocalizationManager.CurrentLanguage = SettingData.LanguageType.Spanish.ToString();
                break;
        }
    }
}
