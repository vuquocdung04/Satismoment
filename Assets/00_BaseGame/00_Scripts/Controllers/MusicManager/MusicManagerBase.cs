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
    public enum SourceAudio { Music, UI, Effect};

    public AudioSource musicSource;
    public AudioSource soundSource;
    [Space(5)]
    public AudioClip clickSound;
    public AudioClip BGMusic;
    public AudioClip winMusic;
    public AudioClip startLevel;

    public void Init()
    {
        musicSource.volume = GameController.Instance.useProfile.OnMusic ? 0.15f : 0;
        soundSource.volume = GameController.Instance.useProfile.OnSound ? 0.15f : 0;
        PlayBGMusic();
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

    public void PlayWinLevelSound()
    {
        if (!GameController.Instance.useProfile.OnMusic) return;
        PlaySingle(winMusic, SourceAudio.UI);
    }
    public void PlayClickSound()
    {
        PlaySingle(clickSound, SourceAudio.UI);
    }

    public void PlayStartLevelSound()
    {
        PlaySingle(startLevel,SourceAudio.Music);
    }

    public void PlaySingle(AudioClip clip, SourceAudio source = SourceAudio.Effect)
    {
        if (clip == null) return;
        switch (source)
        {
            case SourceAudio.Music:
                if (MusicVolume == 0) return;
                musicSource.clip = clip;
                musicSource.Play();
                break;
            case SourceAudio.UI:
                if (SoundVolume == 0) return;
                soundSource.clip = clip;
                soundSource.Play();
                break;
        }
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
