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
            GameController.Instance.musicManager.PlayClickSound();
        });

        btnAdsUnlock.onClick.AddListener(delegate
        {
            Debug.LogError("Ads");
            OnClickAdsUnlock();
            GameController.Instance.musicManager.PlayClickSound();
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
