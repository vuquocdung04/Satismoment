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
            GameController.Instance.musicManager.PlayClickSoundUI();
            OnClickSetting();
        });

        btnSkip.onClick.AddListener(delegate
        {
            GameController.Instance.musicManager.PlayClickSoundUI();
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

    public void HiddenAllButton()
    {
        btnSetting.gameObject.SetActive(false);
        btnSkip.gameObject.SetActive(false);
    }

    public void ShowAllButton()
    {
        btnSetting.gameObject.SetActive(true);
        btnSkip.gameObject.SetActive(true);
    }

}
