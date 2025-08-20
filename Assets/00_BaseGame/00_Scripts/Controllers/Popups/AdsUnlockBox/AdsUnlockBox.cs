using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }

    void InitState()
    {

    }

    void OnClickAdsUnlock()
    {
        UseProfile.MaxUnlockedLevel++;
    }

}
