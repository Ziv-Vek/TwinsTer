using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Twinster.Scenes
{
    public class SplashLoadingSceneHandler : MonoBehaviour
    {
        [SerializeField] float delayLoad = 5f;

        // private void Awake() {
        //     PlayerPrefs.DeleteKey("playerLevel");
        // }
        
        private void Start() {
            StopAllCoroutines();
            StartCoroutine(LoadMainMenu());
        }

        IEnumerator LoadMainMenu()
        {
            yield return new WaitForSeconds(delayLoad);
            
            FindObjectOfType<LevelLoader>().LoadMainMenu();
        }
    
    }
}
