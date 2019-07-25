using UnityEngine;
using VMUnityLib;

/// <summary>
/// SE管理クラス
/// </summary>
public class SeManager : MonoBehaviour
{
    [SerializeField]
    SePlayer sePlayer = default;   // SE再生クラス

    /// <summary>
    /// タップ音
    /// </summary>
    public void PlayTapSE()
    {
        sePlayer.PlaySe("TapSE", GameDataManager.Inst.SettingData.SeVolume, 0.2f);
    }

    /// <summary>
    /// 決定音
    /// </summary>
    public void PlaySelectSE()
    {
        sePlayer.PlaySe("SelectSE", GameDataManager.Inst.SettingData.SeVolume, 0);
    }

    /// <summary>
    /// ウィンドウ展開音
    /// </summary>
    public void PlayWindowOpenSE()
    {
        sePlayer.PlaySe("WindowOpenSE", GameDataManager.Inst.SettingData.SeVolume, 0);
    }

    /// <summary>
    /// キャンセル音
    /// </summary>
    public void PlayCancelSE()
    {
        sePlayer.PlaySe("CancelSE", GameDataManager.Inst.SettingData.SeVolume, 0);
    }
}
