using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Twinster.Scenes
{
    public class TermsMainScreen : MonoBehaviour
    {
        LevelLoader levelLoader;

        private void Awake()
        {
            levelLoader = FindObjectOfType<LevelLoader>();
        }

        void Start()
        {
            if (PlayerPrefs.HasKey("PP12062022") == false || PlayerPrefs.GetInt("PP12062022", 0) == 0)
            {
                PlayerPrefs.SetInt("PP12062022", 0);
                return;
            }
            else
            {
                levelLoader.LoadNextLevel();
            }
        }

        public void AcceptAndContinue()
        {
            PlayerPrefs.SetInt("TermsAccepted", 1);
            levelLoader.LoadNextLevel();
        }

        public void OpenPPURL()
        {
            Application.OpenURL("http://www.wendy-pan.com/privacy-policy");
        }

    }

}
