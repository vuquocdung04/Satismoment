using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            GameController.Instance.musicManager.PlayClickSound();
            OnClickPlay();
        });
    }
    void OnClickPlay()
    {
        UseProfile.SelectedLevel = idLevel;
        if (idLevel <= UseProfile.MaxUnlockedLevel)
            Initiate.Fade(SceneName.GAME_PLAY, Color.black, 2f);
        else if(idLevel == UseProfile.MaxUnlockedLevel + 1)
        {
            AdsUnlockBox.SetUp().Show();
        }

    }


    public void SetStateItem()
    {
        
    }


    protected override void LoadComponents()
    {
        base.LoadComponents();
        btnPlay = GetComponent<Button>();
        iconLevel = transform.Find("img").GetComponent<Image>();
        imgIcon = transform.Find("img").Find("icon").GetComponent<Image>();
    }
}
