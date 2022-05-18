using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Twinster.Bank;

namespace Twinster.Sprites
{
    public class ThemeProduct : MonoBehaviour
    {
        [SerializeField] ThemeSpritesSO theme;
        [SerializeField] Sprite availableImage;
        [SerializeField] Sprite unAvailableImage;
        [SerializeField] int price;

        private void Start() {
            PaintAvailabilityImage();
        }

        private void PaintAvailabilityImage()
        {
            if (theme.GetIsAvailable)
            {
                GetComponent<Image>().sprite = availableImage;    
            }
            else
            {
                GetComponent<Image>().sprite = unAvailableImage;
                GetComponent<Image>().color = Color.black;
            }
            // bool isAvailable = Resources.Load<ThemeSpritesSO>($"Themes/{themeName}").GetIsAvailable;
            // if (!isAvailable)
            // {
            //     GetComponent<Image>().color = Color.black;
            // }
        }

        public void TryPurchase()
        {
            Debug.Log($"You selected: {theme.GetThemeName()}");

            if (FindObjectOfType<StarsBank>().Stars >= price)
            {
                FindObjectOfType<StarsBank>().WithdrawStars(price);
                //FindObjectOfType<SpriteBank>().SetTheme(theme.GetThemeName());
                FindObjectOfType<SpriteBank>().SaveTheme(theme.GetThemeName().ToString());
            }
            else
            {
                // Some feedback purchase failed.
                Debug.Log("You don't have enough stars");
            }
        }
    }

}

