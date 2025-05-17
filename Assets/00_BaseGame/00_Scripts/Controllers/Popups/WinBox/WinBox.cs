using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinBox : BaseBox
{
    static WinBox instance;
    public static WinBox SetUp()
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<WinBox>(PathPrefabs.WIN_BOX));
            instance.Init();
        }
        instance.InitState();
        return instance;
    }

    public Button btnNext;
    public Button btnRestart;
    public Button btnHome;
    public CharactorAnim anim;

    void Init()
    {
        anim.Init();

        btnNext.onClick.AddListener(HandleNext);
        btnRestart.onClick.AddListener(OnClickRestart);
        btnHome.onClick.AddListener(OnClickHome);
    }
    void InitState()
    {

    }

    public void Show2()
    {
        ThumbUpBox.SetUp().Show();
        DOVirtual.DelayedCall(2f, delegate
        {
            ThumbUpBox.SetUp().Close();
        }).OnComplete(()=> Show());
    }


    public void HandleNext()
    {
        Next();

        void Next()
        {
            if(UseProfile.SelectedLevel == UseProfile.MaxUnlockedLevel)
            {
                UseProfile.MaxUnlockedLevel++;
            }
            UseProfile.SelectedLevel++;
            GameController.Instance.musicManager.PlayClickSound();
            Initiate.Fade(SceneName.GAME_PLAY,Color.black,2f);
        }
    }

    void OnClickHome()
    {
        GameController.Instance.musicManager.PlayClickSound();
        Initiate.Fade(SceneName.HOME_SCENE, Color.black, 2f);
    }

    void OnClickRestart()
    {
        GameController.Instance.musicManager.PlayClickSound();
        Initiate.Fade(SceneName.GAME_PLAY, Color.black, 2f);
    }
}
