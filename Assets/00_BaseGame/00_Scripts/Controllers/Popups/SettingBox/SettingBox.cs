using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingBox : BaseBox
{
    static SettingBox instance;
    public static SettingBox SetUp()
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<SettingBox>(PathPrefabs.SETTING_BOX));
            instance.Init();
        }
        instance.InitState();
        return instance;
    }
    public Button btnClose;
    public Button btnGoHome;
    void Init()
    {
        btnClose.onClick.AddListener(delegate
        {
            GameController.Instance.musicManager.PlayClickSound();
            Close();
        });
        btnGoHome.onClick.AddListener(delegate
        {
            GameController.Instance.musicManager.PlayClickSound();
            OnClickGoHome();
        });
    }

    void InitState()
    {

    }

    void OnClickGoHome()
    {
        Initiate.Fade(SceneName.HOME_SCENE,Color.black,1f);
    }
}
