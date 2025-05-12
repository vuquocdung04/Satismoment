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

    void Init()
    {
        btnNext.onClick.AddListener(HandleNext);
    }
    void InitState()
    {

    }

    public void HandleNext()
    {
        Next();

        void Next()
        {
            UseProfile.MaxUnlockedLevel++;
            UseProfile.SelectedLevel++;
            GameController.Instance.musicManager.PlayClickSound();
            Initiate.Fade(SceneName.GAME_PLAY,Color.black,2f);
        }
    }
}
