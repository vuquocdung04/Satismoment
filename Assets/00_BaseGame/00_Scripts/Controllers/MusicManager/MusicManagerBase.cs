
using Sirenix.OdinInspector;
using UnityEngine;


namespace _00_BaseGame._00_Scripts.Controllers.MusicManager
{
    public class MusicManagerBase : SerializedMonoBehaviour
    {
        public enum SourceAudio { Music, Sound};

        public AudioSource musicSource;
        public AudioSource soundSource;
        [Space(5)]
        public AudioClip clickSound;
        public AudioClip bgMusic;
        public AudioClip winMusic;
        public AudioClip startLevel;

        public void Init()
        {
            musicSource.volume = GameController.Instance.useProfile.OnMusic ? 0.5f : 0;
            soundSource.volume = GameController.Instance.useProfile.OnSound ? 0.5f : 0;
            PlayBgMusic();
        }


        private float MusicVolume => GameController.Instance.useProfile.OnMusic ? 0.5f : 0;

        private float SoundVolume => GameController.Instance.useProfile.OnSound ? 0.5f : 0;


        private void PlayBgMusic()
        {
            musicSource.clip = bgMusic;
            musicSource.Play();
        }

        public void PlayWinLevelSound()
        {
            if (!GameController.Instance.useProfile.OnSound) return;
            PlaySingle(winMusic);
        }
        // ReSharper disable Unity.PerformanceAnalysis
        public void PlayClickSound()
        {
            PlaySingle(clickSound);
        }

        public void PlayStartLevelSound()
        {
            PlaySingle(startLevel,false,SourceAudio.Music);
        }

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
            }
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

        public void PauseSound(bool isPause = false)
        {
            if (soundSource.isPlaying)
            {
                if (!isPause) soundSource.Pause();
                else soundSource.Stop();
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
        }
        #endregion
    }
}
