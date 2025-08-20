
using UnityEngine;
using UnityEngine.UI;

public class SettingBox : BaseBox
{
    private static SettingBox instance;
    public static SettingBox SetUp()
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<SettingBox>(PathPrefabs.SETTING_BOX));
            instance.Init();
        }
        instance.InitState();
        return instance;
    }
    public Button btnClose;
    public Button btnSound;
    public Button btnMusic;
    public Button btnVib;

    [Space(10)]
    public Image imgSound;
    public Image imgMusic;
    public Image imgVib;
    [Space(10)]
    public Sprite imgSoundOn;
    public Sprite imgSoundOff;
    public Sprite imgMusicOn;
    public Sprite imgMusicOff;
    public Sprite imgVibOn;
    public Sprite imgVibOff;

    private void Init()
    {
        InitDisplaySoundAndMusic();
        btnClose.onClick.AddListener(delegate
        {
            GameController.Instance.musicManager.PlayUIClick();
            Close();
        });
        btnSound.onClick.AddListener(delegate
        {
            GameController.Instance.musicManager.PlayUIClick();
            OnClickSound();
        });
        btnMusic.onClick.AddListener(delegate
        {
            GameController.Instance.musicManager.PlayUIClick();
            OnMusicSound();
        });
    }

    private void InitState()
    {

    }

    private void OnClickSound()
    {
        if(GameController.Instance.useProfile.OnSound)
        {
            imgSound.sprite = imgSoundOff;
            GameController.Instance.useProfile.OnSound = false;
        }
        else
        {
            imgSound.sprite = imgSoundOn;
            GameController.Instance.useProfile.OnSound = true;
        }
    }

    private void OnMusicSound()
    {
        if (GameController.Instance.useProfile.OnMusic)
        {
            imgMusic.sprite = imgMusicOff;
            GameController.Instance.useProfile.OnMusic = false;
        }
        else
        {
            imgMusic.sprite = imgMusicOn;
            GameController.Instance.useProfile.OnMusic = true;
        }
    }

    private void InitDisplaySoundAndMusic()
    {
        imgSound.sprite = GameController.Instance.useProfile.OnSound ? imgSoundOn : imgSoundOff;
        imgMusic.sprite = GameController.Instance.useProfile.OnMusic ? imgMusicOn : imgMusicOff;
    }
    
}
