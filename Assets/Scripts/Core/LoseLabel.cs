using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Twinster.Scenes;

namespace Twinster.Core
{
    public class LoseLabel : MonoBehaviour
    {
        public void RestartLevel()
        {
            FindObjectOfType<LevelLoader>().RestartLevel();
        }

        public void LoadMainMenu()
        {
            //FindObjectOfType<LevelLoader>().TinySauceGoToMainMenu();
            FindObjectOfType<LevelLoader>().LoadMainMenu();
        }
    }

}
