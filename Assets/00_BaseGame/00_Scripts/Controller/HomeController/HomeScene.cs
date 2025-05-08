using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeScene : MonoBehaviour
{

    public Button btnSetting;
    public Button btnMenu;
    public Button btnOpenShop;
    public Button btnSummon;
    public Button btnWheelSpin;

    public void Init()
    {
        btnMenu.onClick.AddListener(delegate
        {
            MenuBox.SetUp().Show();

        });
    }


}
