using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Twinster.Sprites
{
    public class ThemeStoreArtPresentor : MonoBehaviour
    {
        [SerializeField] ThemeArt[] themeArts;
        [SerializeField] SpriteBank spriteBank;

        [System.Serializable]
        public class ThemeArt
        {
            public string fontName;
            [SerializeField] ThemeNames themeName;
            [SerializeField] public RectTransform themeArtObject;

            public ThemeNames GetThemeName { get { return themeName;  } }
        }

        private void Start()
        {
            DisplayProductArt(spriteBank.GetActiveThemeName());
        }

        private void OnEnable()
        {
            ThemeProduct.onNewProductSelected += DisplayProductArt;
        }

        private void OnDisable()
        {
            ThemeProduct.onNewProductSelected -= DisplayProductArt;
        }

        public void DisplayProductArt(ThemeNames themeToDisplay)
        {
            foreach (ThemeArt art in themeArts)
            {
                if (themeToDisplay != art.GetThemeName)
                {
                    art.themeArtObject.gameObject.SetActive(false);
                    continue;
                }

                art.themeArtObject.gameObject.SetActive(true);
            }
        }
    }
}

