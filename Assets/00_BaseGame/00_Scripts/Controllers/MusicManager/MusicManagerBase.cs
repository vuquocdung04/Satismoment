using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public enum NameMusic
{
    None = 0,
    Fly = 1,
    FlyOut = 2,
    Idle = 3,
    Beep = 4,
}

public class MusicData
{
    public NameMusic name;
    public float spaceDelayTime;
    public AudioClip audioClip;
}

public class MusicManagerBase : SerializedMonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource soundSource;
    [Space(5)]
    public AudioClip clickSound;
    public AudioClip BGMusic;
    public AudioClip winMusic;

    public void Init()
    {

    }


    public float MusicVolume
    {
        get
        {
            return GameController.Instance.useProfile.OnMusic ? 1 : 0;
        }
    }

    public float SoundVolume
    {
        get
        {
            return GameController.Instance.useProfile.OnSound ? 1 : 0;
        }
    }


    public void PlayBGMusic()
    {
        musicSource.clip = BGMusic;
        musicSource.Play();
    }

    public void PlayClickSound()
    {
        //PlaySing
    }

    public void PlaySingle(AudioClip clip)
    {
        if (clip == null) return;
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSoundVolume(float volume)
    {
        soundSource.volume = volume;
    }
}
