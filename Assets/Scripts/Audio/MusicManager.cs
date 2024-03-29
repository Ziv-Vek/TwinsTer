using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Twinster.Core;
using UnityEngine.SceneManagement;

namespace Twinster.Audio
{
    public class MusicManager : MonoBehaviour
    {
        AudioSource myAudioSource = null;
        const string MUSIC_ENABLED = "musicEnabled";
        public static MusicManager instance;

        bool isMusicEnabled = true;

        private void Awake() {
#region SINGLTON
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else{
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            
#endregion SINGLTON            
            
            myAudioSource = GetComponent<AudioSource>();

            if (!PlayerPrefs.HasKey(MUSIC_ENABLED))
            {
                PlayerPrefs.SetInt(MUSIC_ENABLED, 1);
                isMusicEnabled = true;
            }
            else
            {
                if (PlayerPrefs.GetInt(MUSIC_ENABLED, 1) == 1)
                {
                    isMusicEnabled = true;
                }
                else
                {
                    isMusicEnabled = false;
                }
            }
        }

        private void Start() {
            if (isMusicEnabled)
            {
                PlayMusic();
            }
            else
            {
                StopMusic();
            }
        }

        private void OnEnable() {
            LevelSettings.eventLevelComplete += PauseMusic;
            LevelSettings.eventLevelLost += PauseMusic;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable() {
            LevelSettings.eventLevelComplete -= PauseMusic;
            LevelSettings.eventLevelLost -= PauseMusic;

            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public bool GetIsMusicEnabled()
        {
            return isMusicEnabled;
        }

        public void ToggleMusic()
        {
            if (isMusicEnabled)
            {
                StopMusic();
            }
            else
            {
                PlayMusic();
            }
        }

        public void UnPauseMusic()
        {
            myAudioSource.UnPause();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (isMusicEnabled && (myAudioSource.isPlaying == false))
            {
                if (scene.name == "__Main Menu")
                {
                    myAudioSource.time = 0.00f;
                    myAudioSource.Play();
                }
                else
                {
                    myAudioSource.Play();
                }
            }
                
        }

        private void PauseMusic()
        {
            if (myAudioSource.isPlaying)
            {
                myAudioSource.Pause();
            }
        }

        private void StopMusic()
        {
            myAudioSource.Stop();
            isMusicEnabled = false;
            PlayerPrefs.SetInt(MUSIC_ENABLED, 0);
        }

        private void PlayMusic()
        {
            myAudioSource.Play();
            isMusicEnabled = true;
            PlayerPrefs.SetInt(MUSIC_ENABLED, 1);
        }
    }
}
