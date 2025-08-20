
using UnityEngine;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    public Button btnSetting;
    public Button btnSkip;
    public Button btnShowHint;
    public void Init()
    {

        btnSetting.onClick.AddListener(delegate
        {
            GameController.Instance.musicManager.PlayUIClick();
            OnClickSetting();
        });

        btnSkip.onClick.AddListener(delegate
        {
            GameController.Instance.musicManager.PlayUIClick();
            HandleSkipButton();
        });
        
        btnShowHint.onClick.AddListener(delegate
        {
            GameController.Instance.musicManager.PlayUIClick();
            OnClickShowHint(); 
        });

    }

    private void HandleSkipButton()
    {
        WinBox.SetUp().Show();
    }

    private void OnClickSetting()
    {
        SettingGameBox.SetUp().Show();
    }

    private void OnClickShowHint()
    {
        HintBox.SetUp().Show();
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
