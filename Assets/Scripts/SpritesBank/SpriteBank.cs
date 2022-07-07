using System.Collections;
using System.Collections.Generic;
using Twinster.Core;
using UnityEngine;
using Twinster.Saving;
using Twinster.Scenes;
using UnityEngine.Events;

namespace Twinster.Sprites
{
    public class SpriteBank : MonoBehaviour, ISaveable
    {
        [SerializeField] ThemeNames activeTheme = ThemeNames.Generic;
        public ThemeNames ActiveTheme { get { return activeTheme; } }

        [SerializeField] ThemeSpritesSO activeThemeSO;      // serialized for debugging.

        [SerializeField] List<Sprite> activeSet = null;
        
        [Space(5)]
        [Header("Themes SO:")]
        [Space(3)]
        [SerializeField] ThemeSpritesSO[] themes = null;

        [SerializeField] ThemeNames defaultTheme = ThemeNames.Generic;
        [SerializeField] ThemeSets defaultSetName = ThemeSets.All;

        string themeNameString;


        private void Start() {
            Debug.Log("activeThemeSO name is: " + activeThemeSO.GetThemeName());
            if (activeThemeSO == null)
            {
                Debug.Log("You are using the default Theme!");
                activeThemeSO = Resources.Load<ThemeSpritesSO>($"Themes/{defaultTheme}");
            }

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
            activeTheme = (ThemeNames)System.Enum.Parse( typeof(ThemeNames), themeNameString);
            activeThemeSO = Resources.Load<ThemeSpritesSO>($"Themes/{activeTheme}");
            CaptureState();
            FindObjectOfType<SavingWrapper>().Save();
        }

        private void PrepareSpriteSetList()
        {
            LevelSettings levelSettings = FindObjectOfType<LevelSettings>();

            if (levelSettings == null) return;

            ThemeSets chosenSet = levelSettings.GetThemeSet();

            foreach (ThemeSpritesSO.Sets set in activeThemeSO.sets)
            {
                if (set.GetSetName() != chosenSet) continue;

                for (int i = 0; i < set.GetSpriteBankLengh(); i++)
                {
                    activeSet.Add(set.GetSprite(i));
                }

                return;
            }

            /*foreach (ThemeSpritesSO theme in themes)
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
            }*/

            // Chosen set is not present. Default set will be selected and used.
            Debug.LogError("Chosen set is not present. Using default set.");

            foreach (ThemeSpritesSO.Sets set in activeThemeSO.sets)
            {
                if (set.GetSetName() != defaultSetName) continue;

                for (int i = 0; i < set.GetSpriteBankLengh(); i++)
                {
                    activeSet.Add(set.GetSprite(i));
                }

                return;
            }

            /*chosenSet = defaultSetName;
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
            }*/
        }

        public Sprite GetThemeBackground()
        {
            return activeThemeSO.GetThemeBackground();
        }

        public object CaptureState()
        {
            //return activeTheme;
            return themeNameString;
        }

        public void RestoreState(object state)
        {
            Debug.Log("RestoreState is called from SpriteBank");
            //Debug.Log($"Active theme is: {(string)activeTheme}");
            //Debug.Log($"Active theme is: {(ThemeNames)state}");
            //activeTheme = (ThemeNames)state;
            if (state != null)
            {
                activeTheme = (ThemeNames)System.Enum.Parse( typeof(ThemeNames), (string)state);
                activeThemeSO = Resources.Load<ThemeSpritesSO>($"Themes/{activeTheme}");
            }
        }
    }
}

