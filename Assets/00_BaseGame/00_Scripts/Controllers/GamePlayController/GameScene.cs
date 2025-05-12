using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    public Button btnSetting;
    public Button btnSkip;
    public void Init()
    {

        btnSetting.onClick.AddListener(delegate
        {
            GameController.Instance.musicManager.PlayClickSound();
            OnClickSetting();
        });

        btnSkip.onClick.AddListener(delegate
        {
            GameController.Instance.musicManager.PlayClickSound();
            HandleSkipButton();
        });

    }

    public void HandleSkipButton()
    {
        WinBox.SetUp().Show();
    }
    public void OnClickSetting()
    {
        SettingGameBox.SetUp().Show();
    }



}
