using UnityEngine;
using UnityEngine.SceneManagement;

namespace Twinster.Scenes
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] int mainMenuIndex = 1;
        [SerializeField] int firstLevelIndex = 2;
        const string PLAYER_LEVEL = "playerLevel";

        int savedLevel = 0;

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
            SceneManager.LoadScene(mainMenuIndex);
        }
    }
}

