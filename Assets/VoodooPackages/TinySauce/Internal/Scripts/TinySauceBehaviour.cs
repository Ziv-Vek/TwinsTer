using System;
using System.Collections.Generic;
using System.Linq;
using com.adjust.sdk;
using UnityEngine;
using Voodoo.Sauce.Internal.Analytics;
using Voodoo.Sauce.Internal.IdfaAuthorization;
using Facebook.Unity;

namespace Voodoo.Sauce.Internal
{
    internal class TinySauceBehaviour : MonoBehaviour
    {
        private const string TAG = "TinySauce";
        private static TinySauceBehaviour _instance;
        private TinySauceSettings _sauceSettings;
        private bool _advertiserTrackingEnabled;
        private IABTestManager aBTestManager;


        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
                return;
            }
            
            VoodooLog.Initialize(VoodooLogLevel.WARNING);
            
            InitABTest();
            
            #if UNITY_IOS

                NativeWrapper.RequestAuthorization((status) =>
                {
                    _advertiserTrackingEnabled = status == IdfaAuthorizationStatus.Authorized;
                    InitFacebook();
                    InitAnalytics(); // init Voodoo Analytics and GameAnalytics
                    GetComponent<Adjust>().InitAdjust(); // GetComponent to be removed from here in future releases
                });

            #elif UNITY_ANDROID

                InitFacebook();
                // init TinySauce sdk
                InitAnalytics();
                GetComponent<Adjust>().InitAdjust(); // GetComponent to be removed from here in future releases
            #endif

            if (transform != transform.root)
                throw new Exception("TinySauce prefab HAS to be at the ROOT level!");

            _sauceSettings = TinySauceSettings.Load();
            if (_sauceSettings == null)
                throw new Exception("Can't find TinySauce sauceSettings file.");

        }
        

        private void InitCallback()
        {
            if (FB.IsInitialized)
            {
                #if UNITY_IOS
                    FB.Mobile.SetAdvertiserTrackingEnabled(_advertiserTrackingEnabled); // iOS only call, do not need to be done on Android
                    FB.Mobile.SetAdvertiserIDCollectionEnabled(_advertiserTrackingEnabled); 
                #elif UNITY_ANDROID
                    FB.Mobile.SetAdvertiserIDCollectionEnabled(true);
                #endif
                FB.Mobile.SetAutoLogAppEventsEnabled(true);
                
                // Signal an app activation App Event
                FB.ActivateApp();
                // Continue with Facebook SDK
                // ...
            }
            else
            {
                Debug.Log("Failed to Initialize the Facebook SDK");
            }
        }

        private void OnHideUnity(bool isGameShown)
        {
            if (!isGameShown)
            {
                // Pause the game - we will need to hide
                Time.timeScale = 0;
            }
            else
            {
                // Resume the game - we're getting focus again
                Time.timeScale = 1;
            }
        }

        private void InitABTest() //All initializations should be done like this. Would be useful for module/sdk addition/removal
        {
            if(GetAbTestingManager().Count == 0) return;
            aBTestManager = (IABTestManager) Activator.CreateInstance(GetAbTestingManager()[0]);
            aBTestManager.Init();
        }

        private static List<Type> GetAbTestingManager()
        {
            Type interfaceType = typeof(IABTestManager);
            List<Type> AbTest = GetTypes(interfaceType);

            return AbTest;
        }

        private void InitAnalytics()
        {
            VoodooLog.Log(TAG, "Initializing Analytics");
            
            AnalyticsManager.Initialize(_sauceSettings, true);
            
        }
        private void InitFacebook()
        {
            if (!FB.IsInitialized) FB.Init(InitCallback, OnHideUnity);
            
            else InitCallback();         
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            // Brought forward after soft closing 
            // Brought forward after soft closing 
            if (!pauseStatus) {
                AnalyticsManager.OnApplicationResume();
            }
        }
        
        private static List<Type> GetTypes(Type toGetType)
        {
            List<Type> types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => toGetType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                .ToList();

            types.Sort((x, y) => string.Compare(x.Name, y.Name, StringComparison.Ordinal));

            return types;
        }
    }
}