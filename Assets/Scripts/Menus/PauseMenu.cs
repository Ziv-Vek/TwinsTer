using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Twinster.Scenes;

namespace Twinster.Menus
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] GameObject settingsMenuCanvas = null;

        const string MAIN_MENU = "__Main Menu";

        public void UnPause()
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }

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

        public void RestartLevel()
        {
            Time.timeScale = 1;
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }

        public void LoadMainMenu()
        {
            Time.timeScale = 1;
            //FindObjectOfType<LevelLoader>().TinySauceGoToMainMenu();
            SceneManager.LoadScene(MAIN_MENU);
        }
    }
}

