
using Sirenix.OdinInspector;
using UnityEngine;

namespace _00_BaseGame._00_Scripts.Controllers.MusicManager
{
    public class MusicManagerBase : SerializedMonoBehaviour
    {
        public enum SourceAudio { Music, Sound, SoundBackup};

        public AudioSource musicSource;
        public AudioSource soundSource;
        [Header("Du phong")]
        public AudioSource soundBackupSource;
        [Space(5)]
        [Header("Music"), Space(5)]
        public AudioClip bgMusic;
        [Header("Sound UI"), Space(5)]
        public AudioClip clickSound;
        public AudioClip winSound;
        public AudioClip thumbsUpSound;
        public AudioClip startLevel;
        [Header("Sound GamePlay"), Space(5)]
        public AudioClip pickItem;
        public AudioClip placeItemTrue;
        public AudioClip placeItemFalse;
        public void Init()
        {
            musicSource.volume = GameController.Instance.useProfile.OnMusic ? 0.5f : 0;
            soundSource.volume = GameController.Instance.useProfile.OnSound ? 0.5f : 0;
            PlayBgMusic();
        }


        private float MusicVolume => GameController.Instance.useProfile.OnMusic ? 0.5f : 0;

        private float SoundVolume => GameController.Instance.useProfile.OnSound ? 0.5f : 0;


        #region UISound
        private void PlayBgMusic()
        {
            musicSource.clip = bgMusic;
            musicSource.Play();
        }

        public void PlayWinLevelSound()
        {
            if (!GameController.Instance.useProfile.OnSound) return;
            PlaySingle(winSound);
        }
        // ReSharper disable Unity.PerformanceAnalysis
        public void PlayUIClick()
        {
            PlaySingle(clickSound);
        }

        public void PlayUIStart()
        {
            PlaySingle(startLevel,false,SourceAudio.Music);
        }
        public void PlayThumbsUpSound()
        {
            PlaySingle(thumbsUpSound);
        }
        #endregion
        

        #region GameSound
        public void PlayPick()
        {
            PlaySingle(pickItem);
        }

        public void PlayPlace()
        {
            PlaySingle(placeItemTrue);
        }

        public void PlayPlaceMultipe()
        {
            PlayMultiple(placeItemTrue);
        }
        
        public void PlayWrong()
        {
            PlaySingle(placeItemFalse);
        }
        #endregion
        
        // Control
        public void PlaySingle(AudioClip clip, bool isLoopSound = false ,SourceAudio source = SourceAudio.Sound)
        {
            if (clip == null) return;
            switch (source)
            {
                case SourceAudio.Music:
                    if (MusicVolume == 0) return;
                    musicSource.clip = clip;
                    musicSource.Play();
                    break;
                case SourceAudio.Sound:
                    if (SoundVolume == 0) return;
                    soundSource.loop = isLoopSound;
                    soundSource.clip = clip;
                    soundSource.Play();
                    break;
                case SourceAudio.SoundBackup:
                    if (SoundVolume == 0) return;
                    soundBackupSource.loop = isLoopSound;
                    soundBackupSource.clip = clip;
                    soundBackupSource.Play();
                    break;
            }
        }

        public void PlayMultiple(AudioClip clip, float? volumeScale = null)
        {
            if (clip == null) return;
            if (SoundVolume == 0) return;
    
            float actualVolume = volumeScale ?? (SoundVolume / 2f);
            soundSource.PlayOneShot(clip, actualVolume);
        }

        
        

        // Pause Stop Music
        public void PauseMusic(bool isPause = false)
        {
            if (musicSource.isPlaying)
            {
                if (!isPause) musicSource.Pause();
                else musicSource.Stop();
            }
        }

        public void PauseSound(bool isPause = false, SourceAudio source = SourceAudio.Sound)
        {
            switch (source)
            {
                case SourceAudio.Sound:
                    if (soundSource.isPlaying)
                    {
                        if (!isPause) soundSource.Pause();
                        else soundSource.Stop();
                    }
                    break;
                case SourceAudio.SoundBackup:
                    if (soundBackupSource.isPlaying)
                    {
                        if (!isPause) soundBackupSource.Pause();
                        else soundBackupSource.Stop();
                    }
                    break;
            }
            
        }
        public void ResumeMusic()
        {
            if (!musicSource.isPlaying && musicSource.clip != null)
                musicSource.UnPause();
        }

        public void ResumeSound()
        {
            if (!soundSource.isPlaying && soundSource.clip != null)
                soundSource.UnPause();
        }

        
        #region  SetVolume
        public void SetMusicVolume(float volume)
        {
            musicSource.volume = volume;
        }

        public void SetSoundVolume(float volume)
        {
            soundSource.volume = volume;
            soundBackupSource.volume = volume;
        }
        #endregion
    }
}
