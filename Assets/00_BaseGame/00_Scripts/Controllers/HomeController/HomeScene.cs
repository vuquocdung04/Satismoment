using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeScene : MonoBehaviour
{

    public Button btnSetting;
    public Button btnNoAds;
    public void Init()
    {
        btnSetting.onClick.AddListener(delegate
        {
            SettingBox.SetUp().Show();
            GameController.Instance.musicManager.PlayClickSound();
        });
    }




}
