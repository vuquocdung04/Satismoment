
using UnityEngine;
using UnityEngine.UI;



public class HomeScene : MonoBehaviour
{

    public Button btnSetting;
    public Button btnRemoveAds;
    public ZoneCtrl zoneCtrl;
    public void Init()
    {
        
        zoneCtrl.Init();
        btnSetting.onClick.AddListener(delegate
        {
            SettingBox.SetUp().Show();
            GameController.Instance.musicManager.PlayUIClick();
        });
        btnRemoveAds.onClick.AddListener(delegate
        {
            RemoveAdsBox.SetUp().Show();
            GameController.Instance.musicManager.PlayUIClick();
        });
    }

    public void RefreshBoard()
    {
        zoneCtrl.Init();
    }




}
