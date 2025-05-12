using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Level_1Ctrl : MonoBehaviour
{
    public Button btnSwitch;
    public Image imgSwitch;
    public Image imgBG;
    public RectTransform transLight;
    [Space(10)]
    public Sprite iconSwitchOn;

    private void Start()
    {
        btnSwitch.onClick.AddListener(delegate
        {
            imgSwitch.sprite = iconSwitchOn;
            HandleLight();
            GameController.Instance.musicManager.PlayClickSound();
        });
    }

    void HandleLight()
    {
        transLight.DOAnchorPos(new Vector3(0, 700f), 0.4f).OnComplete(delegate
        {
            imgBG.color = new Color32(243, 213, 148, 255);
        });
    }


}
