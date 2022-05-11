using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Twinster.Scenes
{
    public class EndingLabel : MonoBehaviour
    {
        [SerializeField] int mainMenuIndex;
        [SerializeField] GameObject shiningBackground, homeButton;

        void Start()
        {
            LeanTween.rotateAround(shiningBackground, Vector3.forward, 360, 30f).setLoopClamp();        
        }

        public void RestartGame()
        {
            PlayerPrefs.DeleteKey("playerLevel");
            SceneManager.LoadScene(mainMenuIndex);
        }
        
    }
}

