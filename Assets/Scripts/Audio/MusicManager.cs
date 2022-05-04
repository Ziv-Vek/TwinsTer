using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Twinster.Audio
{
    public class MusicManager : MonoBehaviour
    {
        AudioSource myAudioSource = null;
        const string MUSIC_ENABLED = "musicEnabled";
        public static MusicManager instance;


#region SINGLTON
        private void Awake() {
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
        }

        private void Start() {
            if (myAudioSource.isPlaying == false)
            {
                myAudioSource.Play();
            }
        }

        public bool GetIsMusicPlaying()
        {
            bool isMusicPlaying = myAudioSource.isPlaying;
            return isMusicPlaying;
        }

        // private void Start() {
        //     if (!PlayerPrefs.HasKey(MUSIC_ENABLED))
        //     {
        //         PlayerPrefs.SetInt(MUSIC_ENABLED, 1);
        //         if (myAudioSource.isPlaying == false)
        //         {
        //             myAudioSource.Play();
        //         }
        //         else
        //         {
        //             myAudioSource.Play();
        //         }
        //     }
        //     if (myAudioSource.isPlaying == false)
        //     {
        //         myAudioSource.Play();
        //     }
        // }

        // public void ToggleMusic(bool isMusicEnabled)
        // {
        //     if (myAudioSource == null)
        //     {
        //         Debug.LogError("AudioSource component not set");
        //         myAudioSource = GetComponent<AudioSource>();
        //     }

        //     if (isMusicEnabled)
        //     {
        //         myAudioSource.Pause();
        //     }
        //     else
        //     {
        //         myAudioSource.Pause();
        //     }


        // }
    }
}
