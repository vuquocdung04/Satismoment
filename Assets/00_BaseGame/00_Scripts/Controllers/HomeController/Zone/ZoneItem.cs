
using UnityEngine.UI;

public class ZoneItem : LoadAutoComponents
{
    public int idLevel;
    public Button btnPlay;
    public Image iconLevel;
    public Image imgIcon;

    private void Start()
    {
        btnPlay.onClick.AddListener(delegate
        {
            GameController.Instance.musicManager.PlayUIClick();
            OnClickPlay();
        });
    }
    void OnClickPlay()
    {
        UseProfile.CurrentLevel = idLevel;
        if (idLevel <= UseProfile.MaxUnlockedLevel)
        {
            GameController.Instance.ChangeScene(SceneName.GAME_PLAY);
        }
        else if(idLevel == UseProfile.MaxUnlockedLevel + 1)
        {
            AdsUnlockBox.SetUp().Show();
        }

    }
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        btnPlay = GetComponent<Button>();
        iconLevel = transform.Find("img").GetComponent<Image>();
        imgIcon = transform.Find("img").Find("icon").GetComponent<Image>();
    }
}
