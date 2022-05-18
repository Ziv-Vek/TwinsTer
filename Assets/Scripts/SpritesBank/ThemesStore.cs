using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Twinster.Scenes;
using Twinster.Saving;
using System;

namespace Twinster.Sprites
{
    public class ThemesStore : MonoBehaviour, ISaveable
    {
        string themeName = null;

        private void Start() {
            FindObjectOfType<SavingWrapper>().Load();
            //Debug.Log($"Loaded theme is: {}")
        }

        public void SelectTheme(string themeName)
        {
            bool isAvailable = Resources.Load<ThemeSpritesSO>($"Themes/{themeName}").GetIsAvailable;
            if (isAvailable)
            {
                SaveSelectedTheme(themeName);
                
                CaptureState();
            }
            else
            {
                Debug.Log("Theme is unavailable");
            }
        }

        private void SaveSelectedTheme(string themeName)
        {
            this.themeName = themeName;
            CaptureState();
            FindObjectOfType<SavingWrapper>().Save();
        }

        public object CaptureState()
        {
            return ThemeNames.Generic;
        }

        public void RestoreState(object state)
        {
            themeName = (string)state;
        }
    }
}
