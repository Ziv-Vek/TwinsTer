using System.Collections.Generic;
using UnityEngine;

namespace Twinster.Sprites
{
    [CreateAssetMenu(fileName = "NewTheme", menuName = "TwinsTer/New Theme Sprites", order = 0)]
    public class ThemeSpritesSO : ScriptableObject 
    {
        [Space(3)]
        [SerializeField] ThemeNames theme;
        [SerializeField] Sprite background = null;
        [SerializeField] Color32 backgroundColor = Color.white;
    
        public Sets[] sets = null;
        
        [Space(5)]
        [Header("Stats:")]
        [Space(3)]
        [Tooltip("Can the player select to use this theme?")]
        [SerializeField] bool isAvailable = false;
        [Tooltip("Is this is the theme to be used(active)?")]
        [SerializeField] bool isLoaded = false;

        [System.Serializable]
        public class Sets
        {
            //[HideInInspector]
            public string fontName;
            [SerializeField] ThemeSets setName = ThemeSets.All;

            [Space(5)]       
            [Header("Sprites & Images:")]
            [Space(3)]
            [SerializeField] Sprite[] spriteBank;

            public ThemeSets GetSetName()
            {
                return setName;
            }

            public int GetSpriteBankLengh()
            {
                return spriteBank.Length;
            }

            public Sprite GetSprite(int spriteBankPlace)
            {
                return spriteBank[spriteBankPlace];
            }
        }

        public ThemeNames GetThemeName()
        {
            return theme;
        }

        public List<Sprite> GetSpriteSet(ThemeSets targetSet)
        {
            foreach(Sets set in sets){
                if (set.GetSetName() == targetSet)
                {
                    //return set.GetSpriteList();
                }
            }
            
            Debug.LogError("No set was found!");
            return null;
        }

        public Sprite GetThemeBackground()
        {
            return background;
        }

        public Color32 GetBackgroundColor()
        {
            return backgroundColor;
        }

        public void SetAsAvailable()
        {
            isAvailable = true;
        }

        public bool GetIsAvailable {
            get { return isAvailable; }
        }

        public bool GetIsLoaded { 
            get { return isLoaded; }
        }
    }
}
