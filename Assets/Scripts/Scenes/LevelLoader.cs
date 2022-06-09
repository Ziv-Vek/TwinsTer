using UnityEngine;
using UnityEngine.SceneManagement;
using Twinster.Saving;

namespace Twinster.Scenes
{
    public class LevelLoader : MonoBehaviour, ISaveable
    {
        [SerializeField] int mainMenuIndex = 1;
        [SerializeField] int firstLevelIndex = 3;
        [SerializeField] int themesStoreIndex = 2;
        
        [SerializeField] int savedLevel = 0;    // serialized for debugging

        private void Start() {
            FindObjectOfType<SavingWrapper>().Load();
        }

        public void StartGame()
        {
            if (savedLevel < firstLevelIndex)
            {
                savedLevel = firstLevelIndex;
            }

            SceneManager.LoadScene(savedLevel);
    }

        public void LoadNextLevel()
        {
            FindObjectOfType<SavingWrapper>().Save();
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(++currentSceneIndex);
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

