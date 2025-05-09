using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeScene : MonoBehaviour
{

    public Button btnMenu;
    public Button btnSound;
    public Button btnMusic;
    public Button btnNoAds;
    [Space(10)]
    public Sprite spriteOff;
    public Sprite spriteSound;
    public Sprite spriteMusic;
    public void Init()
    {
        btnMenu.onClick.AddListener(delegate
        {
            MenuBox.SetUp().Show();
            GameController.Instance.musicManager.PlayClickSound();
        });

        btnMusic.onClick.AddListener(delegate
        {
            if (GameController.Instance.useProfile.OnMusic)
            {
                btnMusic.image.sprite = spriteOff;
                GameController.Instance.useProfile.OnMusic = false;
            }
            else
            {
                btnMusic.image.sprite = spriteMusic;
                GameController.Instance.useProfile.OnMusic = true;
            }
            GameController.Instance.musicManager.PlayClickSound();
        });

        btnSound.onClick.AddListener(delegate
        {
            if (GameController.Instance.useProfile.OnSound)
            {
                btnSound.image.sprite = spriteOff;
                GameController.Instance.useProfile.OnSound = false;
            }
            else
            {
                btnSound.image.sprite = spriteSound;
                GameController.Instance.useProfile.OnSound = true;
            }
            GameController.Instance.musicManager.PlayClickSound();

        });
    }




}
