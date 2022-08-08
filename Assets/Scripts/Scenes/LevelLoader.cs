using UnityEngine;
using UnityEngine.SceneManagement;
using Twinster.Saving;
using Twinster.Monetization;
using System;
using Twinster.Bank;

namespace Twinster.Scenes
{
    public class LevelLoader : MonoBehaviour, ISaveable
    {
        [SerializeField] int mainMenuIndex = 1;
        [SerializeField] int firstLevelIndex = 3;
        [SerializeField] int themesStoreIndex = 2;
        [SerializeField] int savedLevel = 0;    // serialized for debugging
        [SerializeField] int levelIntervalsToShowAds = 3;

        /* private void Awake()
         {
             if (FindObjectOfType<SavingWrapper>() != null)
             {
                 FindObjectOfType<SavingWrapper>().Load();
             }
         }*/

        private void Start()
        {
            if (FindObjectOfType<SavingWrapper>() != null)
            {
                FindObjectOfType<SavingWrapper>().Load();
            }
        }

        public void TinySauceBeginGame(int levelSaved)
        {
            TinySauce.OnGameStarted(levelSaved.ToString());
        }
        
        public void TinySauceWinLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int currentLevelNum = currentSceneIndex - firstLevelIndex + 1;
            TinySauce.OnGameFinished(true, FindObjectOfType<StarsBank>().Stars, (currentLevelNum + 1).ToString());
        }

        public void TinySauceGoToMainMenu()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (currentSceneIndex < firstLevelIndex) return; 

            int currentLevelNum = currentSceneIndex - firstLevelIndex + 1;
            TinySauce.OnGameFinished(false, FindObjectOfType<StarsBank>().Stars, currentLevelNum.ToString());
        }

        public void StartGame()
        {
            if (savedLevel < firstLevelIndex)
            {
                savedLevel = firstLevelIndex;
            }

            TinySauceBeginGame(savedLevel);

            SceneManager.LoadScene(savedLevel);
    }

        public void LoadNextLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(++currentSceneIndex);
        }

        public void CheckAdDisplay()
        {
            FindObjectOfType<SavingWrapper>().Save();
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int currentLevelNum = currentSceneIndex - firstLevelIndex + 1;
            if ((currentLevelNum % 3) == 0)
            {
                FindObjectOfType<AdManager>().ShowInterstitialAd();
            }
            else
            {
                LoadNextLevel();
            }
        }

        public void SaveLevel()
        {
            if (SceneManager.GetActiveScene().buildIndex + 1 > SceneManager.sceneCountInBuildSettings)
            {
                savedLevel = firstLevelIndex;
            }
            else
            {
                savedLevel = SceneManager.GetActiveScene().buildIndex + 1;
            }

            CaptureState();
        }

        public void RestartLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }

        public void LoadMainMenu()
        {
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            if (savingWrapper != null)
            {
                savingWrapper.Save();
            }

            SceneManager.LoadScene(mainMenuIndex);
        }

        public void LoadThemesStore()
        {
            SceneManager.LoadScene(themesStoreIndex);
        }

        public void RestartWholeGame()
        {
            savedLevel = 0;
            CaptureState();
        }

        public object CaptureState()
        {
            return savedLevel;
        }

        public void RestoreState(object state)
        {
            savedLevel = (int)state;
        }

        public int GetFirstLevelIndex()
        {
            return firstLevelIndex;
        }
    }
}

