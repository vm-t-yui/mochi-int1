using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// スキンクラス
/// </summary>
public class SkinController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI UseSkin = default;                                                          // 使用中の餅スキン表示テキスト

    bool[] isRelease = new bool[(int)SettingData.SkinType.Length];                              // 各スキンの解放フラグ

    // 各スキンの解放スコア
    float[] releaseScore =
    {
        PlayData.ReleaseKouhakuSkinScore,
        PlayData.ReleaseYomogiSkinScore,
        PlayData.ReleaseIchigoSkinScore,
        PlayData.ReleaseKashiwaSkinScore,
        PlayData.ReleaseIsobeSkinScore
    };

    /// <summary>
	/// 起動処理
	/// </summary>
    void OnEnable()
    {
        // 使用スキンを表示
        ChangeUseSkinText(GameDataManager.Inst.SettingData.UseSkin);

        // 条件を満たしているスキンを解放
        ReleaseSkin();

    }

    /// <summary>
    /// 餅スキンの解放
    /// </summary>
    void ReleaseSkin()
    {
        for (int i = 0; i < (int)SettingData.SkinType.Length; i++)
        {
            // 目標スコアを達成するとスキン解放
            if (GameDataManager.Inst.PlayData.TotalScore >= releaseScore[i])
            {
                isRelease[i] = true;
            }
        }
    }

    /// <summary>
    /// 餅スキンセット
    /// NOTE: m.tanaka 餅スキン設定ウィンドウのボタンで呼ぶ関数です
    /// </summary>
    /// <param name="skin">セットするスキン番号</param>
    public void SetMochiSkin(int skin)
    {
        // 解放されている状態ならスキン変更
        if (isRelease[skin])
        {
            // 指定されたスキンにデータを変更
            GameDataManager.Inst.SettingData.UseSkin = (SettingData.SkinType)skin;

            // 表示テキスト変更
            ChangeUseSkinText(GameDataManager.Inst.SettingData.UseSkin);
        }
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
            case SettingData.SkinType.NormalMochi:
                UseSkin.text = "普通の餅";
                break;
            case SettingData.SkinType.KouhakuMochi:
                UseSkin.text = "紅白餅";
                break;
            case SettingData.SkinType.YomogiMochi:
                UseSkin.text = "よもぎ餅";
                break;
            case SettingData.SkinType.IchigoDaihuku:
                UseSkin.text = "いちご大福";
                break;
            case SettingData.SkinType.KashiwaMochi:
                UseSkin.text = "柏餅";
                break;
            case SettingData.SkinType.IsobeMochi:
                UseSkin.text = "磯部餅";
                break;
        }
    }
}