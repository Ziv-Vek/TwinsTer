using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Twinster.Menus
{
    public class PauseGameButton : MonoBehaviour
    {
        [SerializeField] GameObject pauseMenuCanvas = null;

        public void ShowPauseMenu()
        {
            if (pauseMenuCanvas == null)
            {
                Debug.LogError("Pause Menu Canvas was not set!");
                return;
            }

            Time.timeScale = 0;
            pauseMenuCanvas.SetActive(true);
        }
    }
}

