using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Twinster.UI
{
    public class PauseMenu : MonoBehaviour
    {
        Canvas myCanvas;

        private void Awake() {
            myCanvas = GetComponent<Canvas>();
        }

        public void ShowPauseMenu()
        {

        }

        public void ShowSettingsMenu()
        {
            
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}

