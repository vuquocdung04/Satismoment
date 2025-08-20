using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingGameBox : BaseBox
{
    static SettingGameBox instance;
    public static SettingGameBox SetUp()
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<SettingGameBox>(PathPrefabs.SETTING_GAME_BOX));
            instance.Init();
        }
        instance.InitState();
        return instance;
    }

    public Button btnClose;
    public Button btnHome;
    public Button btnNextLevelWithAds;
    public Button btnRestart;
    public Button btnSound;
    public Button btnVib;
    [Space(10)]
    public Image imgSound;

    [Space(10)]
    public Sprite iconSoundOn;
    public Sprite iconSoundOff;
    void Init()
    {
        btnClose.onClick.AddListener(delegate
        {
            Close();
            GameController.Instance.musicManager.PlayUIClick();
        });

        btnHome.onClick.AddListener(delegate
        {
            Initiate.Fade(SceneName.HOME_SCENE, Color.black, 2f);
            GameController.Instance.musicManager.PlayUIClick();
        });

        btnNextLevelWithAds.onClick.AddListener(delegate
        {
            Debug.LogError("Ads");
            OnClickNextLevelWithAds();
            GameController.Instance.musicManager.PlayUIClick();
        });

        btnRestart.onClick.AddListener(delegate
        {
            Initiate.Fade(SceneName.GAME_PLAY, Color.black, 2f);
            GameController.Instance.musicManager.PlayUIClick();
        });

        btnSound.onClick.AddListener(delegate
        {
            OnClickSound();
            GameController.Instance.musicManager.PlayUIClick();
        });
    }

    void InitState()
    {
        if (GameController.Instance.useProfile.OnSound)
        {
            imgSound.sprite = iconSoundOn;
        }
        else
        {
            imgSound.sprite = iconSoundOff;
        }
    }
    void OnClickNextLevelWithAds()
    {
        if (UseProfile.CurrentLevel == UseProfile.MaxUnlockedLevel)
        {
            UseProfile.MaxUnlockedLevel++;
        }
        UseProfile.CurrentLevel++;
        Initiate.Fade(SceneName.GAME_PLAY, Color.black, 2f);
    }


    void OnClickSound()
    {
        if (GameController.Instance.useProfile.OnSound)
        {
            imgSound.sprite = iconSoundOff;
            GameController.Instance.useProfile.OnSound = false;
        }
        else
        {
            imgSound.sprite = iconSoundOn;
            GameController.Instance.useProfile.OnSound = true;
        }
    }
}
