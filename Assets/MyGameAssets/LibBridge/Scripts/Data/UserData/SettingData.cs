/******************************************************************************/
/*!    \brief  ゲームの設定データ.
*******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingData
{
    /// <summary>
    /// 言語の種類
    /// </summary>
    public enum LanguageType
    {
        Japanese = 0,     //日本語
        English,          //英語
        German,           //ドイツ語
        Italian,          //イタリア語
        French,           //フランス語
        Chinese,          //中国語
        Spanish,          //スペイン語
    }

    /// <summary>
    /// 餅のスキンの種類
    /// </summary>
    public enum SkinType
    {
        Nomal = 0,     // 普通の餅
        RedWhite,      // 紅白餅
        Yomogi,        // よもぎ餅
        Strawberry,    // いちご大福
        Kashiwa,       // 柏餅
        Isobe,         // 磯部餅
    }

    [SerializeField]
    float        bgmVolume   = 1;                        // BGMのボリューム
    [SerializeField]
    float        seVolume    = 1;                        // SEのボリューム
    [SerializeField]
    bool         isBgmMute   = false;                    // BGMミュートフラグ
    [SerializeField]
    bool         isSeMute    = false;                    // SEミュートフラグ
    [SerializeField]
    LanguageType useLanguage = LanguageType.Japanese;    // 使用中の言語
    [SerializeField]
    SkinType     useSkin     = SkinType.Nomal;           // 使用中の餅のスキン

    /// <summary>
    /// 各データのプロパティ
    /// </summary>
    public float        BgmVolume   { get { return bgmVolume; }   set { bgmVolume = value; } }
    public float        SeVolume    { get { return seVolume; }    set { seVolume = value; } }
    public bool         IsBgmMute   { get { return isBgmMute; }   set { isBgmMute = value; } }
    public bool         IsSeMute    { get { return isSeMute; }    set { isSeMute = value; } }
    public LanguageType UseLanguage { get { return useLanguage; } set { useLanguage = value; } }
    public SkinType     UseSkin     { get { return useSkin; }     set { useSkin = value; } }
}