using UnityEngine;
using UnityEngine.UI;
using EventDispatcher;

public class AdsUnlockBox : BaseBox
{
    static AdsUnlockBox instance;
    public static AdsUnlockBox SetUp()
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<AdsUnlockBox>(PathPrefabs.ADS_UNLOCK_BOX));
            instance.Init();
        }
        instance.InitState();
        return instance;
    }

    public Button btnClose;
    public Button btnAdsUnlock;

    void Init()
    {
        btnClose.onClick.AddListener(delegate
        {
            Close();
            GameController.Instance.musicManager.PlayUIClick();
        });

        btnAdsUnlock.onClick.AddListener(delegate
        {
            Debug.LogError("Ads");
            OnClickAdsUnlock();
            GameController.Instance.musicManager.PlayUIClick();
        });

        // Đăng ký lắng nghe event nhận thưởng
        this.RegisterListener(EventID.REWARDED_ADS_COMPLETED, OnRewardedAdCompleted);
    }

    void OnDestroy()
    {
        // Hủy đăng ký khi object bị hủy
        this.RemoveListener(EventID.REWARDED_ADS_COMPLETED, OnRewardedAdCompleted);
    }

    void InitState()
    {
    }

    void OnClickAdsUnlock()
    {
        // Hiển thị quảng cáo nhận thưởng
        GameController.Instance.adsController.ShowRewardedAd();
    }

    // Xử lý khi nhận thưởng thành công
    private void OnRewardedAdCompleted(object param)
    {
        UseProfile.MaxUnlockedLevel++;
        Debug.Log("Reward received! Unlocking level..." + UseProfile.MaxUnlockedLevel);
        HomeController.Instance.homeScene.RefreshBoard();
        Close();
    }
}