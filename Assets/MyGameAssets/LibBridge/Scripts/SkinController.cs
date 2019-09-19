using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// スキンクラス
/// </summary>
public class SkinController : MonoBehaviour
{
    [SerializeField]
    SkinSpriteSetter skinSpriteSetter = default;

    [SerializeField]
    Transform skinButtons = default;                                 // スキンのボタン

    bool[] isRelease = new bool[(int)SettingData.SkinType.Length];   // 各スキンの解放フラグ

    /// <summary>
	/// 起動処理
	/// </summary>
    void OnEnable()
    {
        // 使用スキンを表示
        skinSpriteSetter.ChangeUseSkin(GameDataManager.Inst.SettingData.UseSkin);

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
            Transform skinImages = skinButtons.GetChild(i).GetChild(1);

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
                skinButtons.GetChild(i).GetChild(0).gameObject.SetActive(true);
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
            skinButtons.GetChild(num).GetChild(0).gameObject.SetActive(false);

            // 表示テキスト変更
            skinSpriteSetter.ChangeUseSkin(GameDataManager.Inst.SettingData.UseSkin);
        }
    }
}