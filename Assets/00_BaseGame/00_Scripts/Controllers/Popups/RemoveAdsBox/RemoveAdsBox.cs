
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RemoveAdsBox : BaseBox
{
    static RemoveAdsBox instance;
    public static RemoveAdsBox SetUp()
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<RemoveAdsBox>(PathPrefabs.REMOVE_ADS_BOX));
            instance.Init();
        }
        instance.InitState();
        return instance;
    }

    public Button btnClose;
    public Button btnRemoveAds;
    public TextMeshProUGUI txtBtnRemoveAds;
    public Sprite spriteRemoved;
    public Sprite spriteDefault;
    private void Init()
    {
        SetStateBtnRemoveAds();
        btnClose.onClick.AddListener(delegate
        {
            Close();
            GameController.Instance.musicManager.PlayUIClick();
        });
        btnRemoveAds.onClick.AddListener(delegate
        {
            HandleRemoveAds();
            GameController.Instance.musicManager.PlayUIClick();
        });
    }

    private void InitState()
    {
        
    }

    private void HandleRemoveAds()
    {
        if (!GameController.Instance.useProfile.IsRemoveAds)
        {
            GameController.Instance.useProfile.IsRemoveAds = true;
            SetStateBtnRemoveAds();
        }
    }

    private void SetStateBtnRemoveAds()
    {
        if (!GameController.Instance.useProfile.IsRemoveAds)
        {
            btnRemoveAds.image.sprite = spriteDefault;
            txtBtnRemoveAds.text = "Remove";
            btnRemoveAds.enabled = true;
        }
        else
        {
            txtBtnRemoveAds.text = "Removed";
            btnRemoveAds.image.sprite = spriteRemoved;
            btnRemoveAds.enabled = false;
        }
    }
}
