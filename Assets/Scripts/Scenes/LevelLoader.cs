using UnityEngine;
using UnityEngine.SceneManagement;
using Twinster.Saving;

namespace Twinster.Scenes
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] int mainMenuIndex = 1;
        [SerializeField] int firstLevelIndex = 2;
        [SerializeField] int themesStoreIndex = 2;
        const string PLAYER_LEVEL = "playerLevel";

        int savedLevel = 0;

        private void Start() {
            FindObjectOfType<SavingWrapper>().Load();
        }

        public void StartGame()
        {
            if (PlayerPrefs.GetInt(PLAYER_LEVEL) < firstLevelIndex || !PlayerPrefs.HasKey(PLAYER_LEVEL))
            {
                PlayerPrefs.SetInt(PLAYER_LEVEL, firstLevelIndex);
            }
            
            int levelIndexToLoad = PlayerPrefs.GetInt(PLAYER_LEVEL);
            SceneManager.LoadScene(levelIndexToLoad);
        }

        public void LoadNextLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(++currentSceneIndex);
        }

        public void SaveLevel()
        {
            int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
            PlayerPrefs.SetInt(PLAYER_LEVEL, nextLevelIndex);
            if (nextLevelIndex + 1 > SceneManager.sceneCountInBuildSettings)
            {
                PlayerPrefs.DeleteKey(PLAYER_LEVEL);
            }
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
    }
}

