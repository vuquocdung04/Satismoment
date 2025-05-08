using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot_MenuBox : LoadAutoComponents
{
    public int iD;
    public Image panel;
    public TextMeshProUGUI txtLevel;
    public Button btnPlay;

    private void Start()
    {
        if (GameController.Instance.levelGame < iD) return;
        btnPlay.onClick.AddListener(this.StartGame);
    }

    private void StartGame()
    {
        Initiate.Fade(SceneName.GAME_PLAY, Color.black,1.5f);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.panel = transform.Find("Panel").GetComponent<Image>();
        this.btnPlay = GetComponent<Button>();
        this.txtLevel = transform.Find("txtLevel").GetComponent<TextMeshProUGUI>();
    }

    public void SetStatePanel()
    {
        this.panel.gameObject.SetActive(false);
    }

}
