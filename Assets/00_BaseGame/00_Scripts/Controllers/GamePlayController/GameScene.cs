using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    public Button btnRetry;
    public Button btnPauseMenu;
    public Button btnNextLevel;

    [Space(10)]
    public TextMeshProUGUI txtLevel;
    public void Init()
    {
        txtLevel.text = UseProfile.SelectedLevel.ToString();

        btnRetry.onClick.AddListener(delegate
        {
            GameController.Instance.musicManager.PlayClickSound();
            OnClickRetry();
        });

        btnPauseMenu.onClick.AddListener(delegate
        {
            GameController.Instance.musicManager.PlayClickSound();
            OnClickMenuPause();
        });

        btnNextLevel.onClick.AddListener(delegate
        {
            GameController.Instance.musicManager.PlayClickSound();
            HandleNextLevelBtn();
        });

    }

    public void HandleNextLevelBtn()
    {
        WinBox.SetUp().Show();
    }

    public void OnClickRetry()
    {
        Initiate.Fade(SceneName.GAME_PLAY,Color.black,1f);
    }

    public void OnClickMenuPause()
    {
        SettingBox.SetUp().Show();
    }



}
