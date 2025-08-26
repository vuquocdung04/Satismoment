
using UnityEngine;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    public Button btnSetting;
    public Button btnSkip;
    public Button btnShowHint;
    public Button btnRemoveAds;
    public void Init()
    {

        btnSetting.onClick.AddListener(delegate
        {
            GameController.Instance.musicManager.PlayUIClick();
            SettingGameBox.SetUp().Show();
        });

        btnSkip.onClick.AddListener(delegate
        {
            GameController.Instance.musicManager.PlayUIClick();
            WinBox.SetUp().Show();
        });
        
        btnShowHint.onClick.AddListener(delegate
        {
            GameController.Instance.musicManager.PlayUIClick();
            HintBox.SetUp().Show();
        });
        btnRemoveAds.onClick.AddListener(delegate
        {
            GameController.Instance.musicManager.PlayUIClick();
            RemoveAdsBox.SetUp().Show();

        });

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
