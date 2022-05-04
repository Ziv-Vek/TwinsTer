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

        private void OnEnable() {
            isMusicEnabled = FindObjectOfType<MusicManager>().GetIsMusicPlaying();
        }

        void PlaceCancelBars()
        {
            musicCancelBar.enabled = isMusicEnabled;
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
            SFXCancelBar.enabled = !SFXCancelBar.enabled;
            
        }

        public void ToggleMusic()
        {
            musicCancelBar.enabled = !musicCancelBar.enabled;
            //FindObjectOfType<MusicManager>
        }
    }
}

