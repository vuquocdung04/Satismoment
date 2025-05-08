using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBox : BaseBox
{
    static MenuBox instance;
    public static MenuBox SetUp()
    {
        if(instance == null)
        {
            instance = Instantiate(Resources.Load<MenuBox>(PathPrefabs.MENU_BOX));
            instance.Init();
        }
        instance.InitState();
        return instance;
    }

    public Button btnClose;
    void Init()
    {
        btnClose.onClick.AddListener(Close);
    }
    void InitState()
    {

    }
}
