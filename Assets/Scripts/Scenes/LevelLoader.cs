using UnityEngine;
using UnityEngine.SceneManagement;

namespace Twinster.Scenes
{
    public class LevelLoader : MonoBehaviour
    {
        public void LoadGame()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(++currentSceneIndex);
        }
    }
}

