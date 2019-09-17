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
    TextMeshProUGUI UseSkin = default;                               // 使用中の餅スキン表示テキスト

    [SerializeField]
    Transform skinButtons = default;                                 // スキンのボタン

    bool[] isRelease = new bool[(int)SettingData.SkinType.Length];   // 各スキンの解放フラグ

    /// <summary>
	/// 起動処理
	/// </summary>
    void OnEnable()
    {
        // 使用スキンを表示
        ChangeUseSkinText(GameDataManager.Inst.SettingData.UseSkin);

        // スキンの解放
        ReleaseSkin();
    }

    /// <summary>
    /// スキンの解放
    /// </summary>
    void ReleaseSkin()
    {
        // 最初の餅を解放されていなかったら解放させる
        if(!GameDataManager.Inst.PlayData.IsReleasedSkin[0])
        {
            GameDataManager.Inst.PlayData.IsReleasedSkin[0] = true;
        }


        for (int i = 0; i < (int)SettingData.SkinType.Length; i++)
        {
            // スキンのイメージの親 (子(0)がシルエット、子(1)がスキン)
            Transform skinImages = skinButtons.GetChild(i).GetChild(2);

            // それぞれのスキン解放フラグがONになっていたらスキンを解放させる
            if (GameDataManager.Inst.PlayData.IsReleasedSkin[i])
            {
                isRelease[i] = true;

                // スキンを表示
                skinImages.GetChild(0).gameObject.SetActive(false);
                skinImages.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                // シルエットを表示
                skinImages.GetChild(0).gameObject.SetActive(true);
                skinImages.GetChild(1).gameObject.SetActive(false);
            }

            // Newアイコンが表示されていないなら表示
            if (GameDataManager.Inst.PlayData.IsDrawSkinNewIcon[i])
            {
                skinButtons.GetChild(i).GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    /// <summary>
    /// 餅スキンセット
    /// NOTE: m.tanaka 餅スキン設定ウィンドウのボタンで呼ぶ関数です
    /// </summary>
    /// <param name="num">セットするスキン番号</param>
    public void SetMochiSkin(int num)
    {
        // 解放されている状態ならスキン変更
        if (isRelease[num])
        {
            // 指定されたスキンにデータを変更
            GameDataManager.Inst.SettingData.UseSkin = (SettingData.SkinType)num;

            // Newアイコンを非表示
            GameDataManager.Inst.PlayData.IsDrawSkinNewIcon[num] = false;
            skinButtons.GetChild(num).GetChild(1).gameObject.SetActive(false);

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