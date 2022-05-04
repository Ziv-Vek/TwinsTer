using System;
using System.Collections;
using System.Collections.Generic;
using Twinster.Selection;
using UnityEngine;

namespace Twinster.Audio
{
    public class SoundEffectsManager : MonoBehaviour
    {
        [SerializeField] AudioClip successfullTwin, starsFalling;
        
        // const string SFX_ENABLED = "SFXenabled";
        AudioSource myAudioSource = null;
        public static SoundEffectsManager instance;
        
        private void OnEnable() {
            SlotsSelectionHandler.successfulTwins += PlayTwinSFX;
        }

        private void OnDisable() {
            SlotsSelectionHandler.successfulTwins -= PlayTwinSFX;
        }

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

        void PlayTwinSFX()
        {
            myAudioSource.PlayOneShot(successfullTwin);
        }

        // private void Start() {
        //     InitializeSFX();
        // }

        

        // public void ToggleSoundEffects()
        // {
        //     if (!PlayerPrefs.HasKey(SFX_ENABLED)
        //     {
        //         InitializeSFX();
        //     }
        //     //.GetInt("SFXenabled") == null 
        //     )
        //     if (PlayerPrefs.SetInt("")
        // }
        
        // private void InitializeSFX()
        // {
        //     if (!PlayerPrefs.HasKey(SFX_ENABLED))
        //     {
        //         PlayerPrefs.SetInt(SFX_ENABLED, 1);
        //     }
        // }

        
    }
}

