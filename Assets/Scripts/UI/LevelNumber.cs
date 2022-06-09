using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Twinster.Scenes;
using UnityEngine.SceneManagement;

namespace Twinster.UI
{
    public class LevelNumber : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI levelText;

        void Start()
        {
            int firstLevel = FindObjectOfType<LevelLoader>().GetFirstLevelIndex();
            int currentLevel = SceneManager.GetActiveScene().buildIndex - firstLevel + 1;
            levelText.text = $"Level {currentLevel}";
        }
    }
}
