using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseProfile : MonoBehaviour
{
    public static int MaxUnlockedLevel
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.MAX_UNLOCK_LEVEL,1);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.MAX_UNLOCK_LEVEL, value);
            PlayerPrefs.Save();
        }
    }
    
    public static int SelectedLevel
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.SELECTED_LEVEL, 1);
        }
        set
        {
            PlayerPrefs.SetInt (StringHelper.SELECTED_LEVEL, value);
            PlayerPrefs.Save();
        }
    }

    public bool OnSound
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.ONOFF_SOUND,1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.ONOFF_SOUND, value ? 1 : 0);
            GameController.Instance.musicManager.SetSoundVolume(value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public bool OnMusic
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.ONOFF_MUSIC,1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.ONOFF_MUSIC, value ? 1 : 0);
            GameController.Instance.musicManager.SetMusicVolume(value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
}
