using System.Collections;
using System.Collections.Generic;
using Twinster.Scenes;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Twinster.Monetization
{
    public class AdManager : MonoBehaviour, IUnityAdsListener
    {
        [SerializeField] bool testMode = true;

#if UNITY_STANDALONE_WIN
        string gameId = "";
#elif UNITY_ANDROID
        string gameId = "4793633";
#elif UNITY_IOS
        string gameId = "4793632";
#endif

        public void OnUnityAdsDidError(string message)
        {
            Debug.LogError($"Unity Ads Error: {message}.");
        }

        public void ShowInterstitialAd()
        {
            Advertisement.Show("InterstitialVideo");
        }

        private void Awake()
        {
            Advertisement.AddListener(this);
            Advertisement.Initialize(gameId, testMode);
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            switch(showResult)
            {
                case ShowResult.Finished:
                    Debug.Log($"Ad {placementId} finished successfuly.");
                    FindObjectOfType<LevelLoader>().LoadNextLevel();
                    break;
                case ShowResult.Failed:
                    Debug.LogWarning($"Ad {placementId} failed.");
                    break;
                case ShowResult.Skipped:
                    Debug.Log($"Ad {placementId} was skipped.");
                    FindObjectOfType<LevelLoader>().LoadNextLevel();
                    break;
            }
        }

        public void OnUnityAdsDidStart(string placementId)
        {
            Debug.Log($"Ad {placementId} Started.");
        }

        public void OnUnityAdsReady(string placementId)
        {
            Debug.Log($"Ad {placementId} is ready.");
        }
    }
}

