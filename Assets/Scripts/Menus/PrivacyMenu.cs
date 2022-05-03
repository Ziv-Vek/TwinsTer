using UnityEngine;

namespace Twinster.Menus
{
    public class PrivacyMenu : MonoBehaviour
    {
        [SerializeField] GameObject settingsMenuCanvas = null;

        public void ShowSettingsMenu()
        {
            if (settingsMenuCanvas == null)
            {
                Debug.LogError("Settings Menu canvas is not set!");
                return;
            }
            settingsMenuCanvas.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}