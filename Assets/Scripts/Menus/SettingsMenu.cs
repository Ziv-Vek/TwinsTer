using UnityEngine;
using UnityEngine.UI;
using Twinster.Audio;

namespace Twinster.Menus
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] GameObject canvasWhenCloses = null;
        [SerializeField] GameObject privacyMenuCanvas = null;
        [SerializeField] Image SFXCancelBar, musicCancelBar;

        bool isMusicEnabled;
        bool isSFXEnabled;

        private void Start() {
            isMusicEnabled = FindObjectOfType<MusicManager>().GetIsMusicEnabled();
            isSFXEnabled = FindObjectOfType<SoundEffectsManager>().GetIsSFXEnabled();
            PlaceCancelBars();
        }

        public void ShowPreviousMenu()
        {
            if (canvasWhenCloses == null)
            {
                Debug.LogError("No previous canvas is set when close button is pressed!");
                return;
            }
            canvasWhenCloses.SetActive(true);
            gameObject.SetActive(false);
        }

        public void ShowPrivacyMenu()
        {
            if (privacyMenuCanvas == null)
            {
                Debug.LogError("Privacy Menu canvas is not set!");
                return;
            }
            privacyMenuCanvas.SetActive(true);
            gameObject.SetActive(false);
        }

        public void ToggleSFX()
        {
            isSFXEnabled = !isSFXEnabled;
            PlaceCancelBars();
            FindObjectOfType<SoundEffectsManager>().ToggleSFX();
        }

        public void ToggleMusic()
        {
            isMusicEnabled = !isMusicEnabled;
            PlaceCancelBars();
            FindObjectOfType<MusicManager>().ToggleMusic();
        }

        void PlaceCancelBars()
        {
            musicCancelBar.enabled = !isMusicEnabled;
            SFXCancelBar.enabled = !isSFXEnabled;
        }
    }
}

