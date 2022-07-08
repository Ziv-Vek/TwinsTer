using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Twinster.Saving;
using System;
using Twinster.Scenes;

namespace Twinster.Sprites
{
    public class ThemesData : MonoBehaviour, ISaveable, ISerializationCallbackReceiver
    {
        public List<ThemeNames> keys = new List<ThemeNames> { };
        public List<bool> values = new List<bool> { };
        
        public Dictionary<ThemeNames, bool> themesAvailabilityData = new Dictionary<ThemeNames, bool>();  // Serialized for debugging

        private void Start()
        {
            Debug.Log($"ThemesData count is: {themesAvailabilityData.Count}");
            if (themesAvailabilityData.Count == 0)
            {
                Debug.Log("ThemesData initializes.");
                PopulateInitializedThemesData();
            }
        }

        private void PopulateInitializedThemesData()
        {
            int themeNamesLenght = Enum.GetValues(typeof(ThemeNames)).Length;
            // Makes the Default theme available
            themesAvailabilityData.Add((ThemeNames)Enum.ToObject(typeof(ThemeNames), 0), true);
            // Makes all other themes unavailable
            for (int i = 1; i <= themeNamesLenght - 1; i++)
            {
               themesAvailabilityData.Add((ThemeNames)Enum.ToObject(typeof(ThemeNames), i), false);
            }
        }

        public void ChangeThemeAvailability(ThemeNames theme, bool newStatus)
        {
            themesAvailabilityData[theme] = newStatus;
            CaptureState();
        }

        public bool GetIsAvailable(ThemeNames theme)
        {
            return themesAvailabilityData[theme];
        }

        public object CaptureState()
        {
            return themesAvailabilityData;
        }

        public void RestoreState(object state)
        {
            if (state == null)
            {
                PopulateInitializedThemesData();
                CaptureState();
                FindObjectOfType<SavingWrapper>().Save();
                return;
            }
            else
            {
                themesAvailabilityData = (Dictionary<ThemeNames, bool>)state;
            }
        }

        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();

            foreach (var kvp in themesAvailabilityData)
            {
                keys.Add(kvp.Key);
                values.Add(kvp.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            themesAvailabilityData = new Dictionary<ThemeNames, bool>();

            for (int i = 0; i != Mathf.Min(keys.Count, values.Count); i++)
            {
                themesAvailabilityData.Add(keys[i], values[i]);
            }
        }
    }
}