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
    public Image panel;
    public Transform posSpawn;
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

    protected override void DoAppear()
    {
        ThumbUpBox.SetUp().Show();
        DOVirtual.DelayedCall(2f, delegate
        {
            ThumbUpBox.SetUp().Close();
            panel.color = new Color32(0,0,0,215);
            GameController.Instance.confettiEffectController.SpawmEffect_Drop_UI(posSpawn,true);
            base.DoAppear();
        });
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
