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
        
        const string SFX_ENABLED = "SFXenabled";
        AudioSource myAudioSource = null;
        public static SoundEffectsManager instance;

        bool isSFXEnabled = true;

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

            if (!PlayerPrefs.HasKey(SFX_ENABLED))
            {
                PlayerPrefs.SetInt(SFX_ENABLED, 1);
                isSFXEnabled = true;
            }
            else
            {
                if (PlayerPrefs.GetInt(SFX_ENABLED, 1) == 1)
                {
                    isSFXEnabled = true;
                }
                else
                {
                    isSFXEnabled = false;
                }
            }
        }

        private void OnEnable() {
            SlotsSelectionHandler.successfulTwins += PlayTwinSFX;
            SlotsSelectionHandler.successfulTriplets += PlayTwinSFX;
        }

        private void OnDisable() {
            SlotsSelectionHandler.successfulTwins -= PlayTwinSFX;
            SlotsSelectionHandler.successfulTriplets -= PlayTwinSFX;
        }

        public bool GetIsSFXEnabled()
        {
            return isSFXEnabled;
        }

        public void ToggleSFX()
        {
            isSFXEnabled = !isSFXEnabled;
            
            if (isSFXEnabled)
            {
                PlayerPrefs.SetInt(SFX_ENABLED, 1);
            }
            else
            {
                PlayerPrefs.SetInt(SFX_ENABLED, 0);
            }
        }

        void PlayTwinSFX()
        {
            if (isSFXEnabled)
            {
                myAudioSource.PlayOneShot(successfullTwin);
            }
        }

        
    }
}

