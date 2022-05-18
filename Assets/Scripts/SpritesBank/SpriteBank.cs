using System.Collections;
using System.Collections.Generic;
using Twinster.Core;
using UnityEngine;
using Twinster.Saving;
using Twinster.Scenes;

namespace Twinster.Sprites
{
    public class SpriteBank : MonoBehaviour, ISaveable
    {
        [SerializeField] ThemeNames activeTheme = ThemeNames.Generic;
        [SerializeField] List<Sprite> activeSet = null;
        
        [Space(5)]
        [Header("Themes SO:")]
        [Space(3)]
        [SerializeField] ThemeSpritesSO[] themes = null;

        [SerializeField] ThemeNames defaultTheme = ThemeNames.Generic;
        [SerializeField] ThemeSets defaultSetName = ThemeSets.All;

        string themeNameString;

        private void Start() {
            activeSet.Clear();
            PrepareSpriteSetList();
        }

        public Sprite GetSprite()
        {
            int randNum = Random.Range(0, activeSet.Count);
            Sprite sprite = activeSet[randNum];
            activeSet.RemoveAt(randNum);
            return sprite;
        }

        public void SetTheme(ThemeNames theme)
        {
            activeTheme = theme;
            
            //CaptureState();
        }

        public void SaveTheme(string themeNameString)
        {
            this.themeNameString = themeNameString;
            activeTheme = (ThemeNames)System.Enum.Parse( typeof(ThemeNames), themeNameString );
            CaptureState();
            FindObjectOfType<SavingWrapper>().Save();
        }

        private void PrepareSpriteSetList()
        {
            if (FindObjectOfType<LevelSettings>() == null) return;

            ThemeSets chosenSet = FindObjectOfType<LevelSettings>().GetThemeSet();

            foreach (ThemeSpritesSO theme in themes)
            {
                if (theme.GetThemeName() != activeTheme) continue;

                foreach (ThemeSpritesSO.Sets set in theme.sets)
                {
                    if (set.GetSetName() != chosenSet) continue;

                    for (int i = 0 ; i < set.GetSpriteBankLengh() ; i++)
                    {
                        activeSet.Add(set.GetSprite(i));
                    }

                    return;
                }
            }

            // Chosen set is not present. Default set will be selected and used.
            Debug.LogError("Chosen set is not present. Using default set.");
            chosenSet = defaultSetName;
            foreach (ThemeSpritesSO theme in themes)
            {
                if (theme.GetThemeName() != activeTheme) continue;

                foreach (ThemeSpritesSO.Sets set in theme.sets)
                {
                    if (set.GetSetName() != chosenSet) continue;

                    for (int i = 0 ; i < set.GetSpriteBankLengh() ; i++)
                    {
                        activeSet.Add(set.GetSprite(i));
                    }

                    return;
                }
            }
        }

        public Sprite GetThemeBackground()
        {
            foreach (ThemeSpritesSO theme in themes)
            {
                if (theme.GetThemeName() == activeTheme)
                {
                    return theme.GetThemeBackground();
                }
            }

            Debug.LogError("Theme or background not set");
            return null;
        }

        public object CaptureState()
        {
            //return activeTheme;
            return themeNameString;
        }

        public void RestoreState(object state)
        {
            Debug.Log("RestoreState is called");
            //Debug.Log($"Active theme is: {(string)activeTheme}");
            //Debug.Log($"Active theme is: {(ThemeNames)state}");
            //activeTheme = (ThemeNames)state;
            if (state != null)
            {
                activeTheme = (ThemeNames)System.Enum.Parse( typeof(ThemeNames), (string)state);
            }
        }
    }
}

