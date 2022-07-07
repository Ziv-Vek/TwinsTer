using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Twinster.Bank;
using TMPro;
using System;
using UnityEngine.Events;

namespace Twinster.Sprites
{
    public class ThemeProduct : MonoBehaviour
    {
        [Space(3)]
        [Header("Config:")]
        [SerializeField] ThemeSpritesSO theme;
        [SerializeField] int price;
        [SerializeField] Sprite availableSprite, unavailableSprite;
        [Space(5)]
        [Header("Cached Ref:")]
        [SerializeField] TextMeshProUGUI priceText;
        [SerializeField] GameObject forPurchaseStatus, activeStatus, availableStatus;
        [SerializeField] Image productImage, activeBackground;
        [SerializeField] SpriteBank spriteBank;
        [SerializeField] StarsBank starBank;

        // Stats:
        ProductStatus currentStatus;
        
        // Events:
        public static event Action<ThemeNames> onNewProductSelected;


        enum ProductStatus
        {
            Active,
            AvailableForSelection,
            AvailableForPurchase,
            NotEnoughStars
        }

        private void Start() {
            UpdateProductStatus();
        }

        private void OnEnable()
        {
            ThemeProduct.onNewProductSelected += RemoveActiveStatus;
            starBank.onBankWithdraw += UpdateProductStatus;
        }

        private void OnDisable()
        {
            ThemeProduct.onNewProductSelected -= RemoveActiveStatus;
            starBank.onBankWithdraw -= UpdateProductStatus;
        }

        private void UpdateProductStatus()
        {
            if (theme.GetIsAvailable)
            {
                if (theme.GetThemeName() == spriteBank.ActiveTheme)
                {
                    currentStatus = ProductStatus.Active;
                }
                else
                {
                    currentStatus = ProductStatus.AvailableForSelection;
                }
            }
            else
            {
                if (starBank.Stars >= price)
                {
                    currentStatus = ProductStatus.AvailableForPurchase;
                }
                else
                {
                    currentStatus = ProductStatus.NotEnoughStars;
                }
            }

            SetProductStatus(currentStatus);
        }

            // bool isAvailable = Resources.Load<ThemeSpritesSO>($"Themes/{themeName}").GetIsAvailable;

        private void SetProductStatus(ProductStatus status)
        {
            switch (currentStatus)
            {
                case ProductStatus.Active:
                    productImage.sprite = availableSprite;
                    forPurchaseStatus.SetActive(false);
                    availableStatus.SetActive(false);
                    activeStatus.SetActive(true);
                    activeBackground.enabled = true;
                    break;
                case ProductStatus.AvailableForSelection:
                    productImage.sprite = availableSprite;
                    forPurchaseStatus.SetActive(false);
                    availableStatus.SetActive(true);
                    activeStatus.SetActive(false);
                    activeBackground.enabled = false;
                    break;
                case ProductStatus.AvailableForPurchase:
                    productImage.sprite = unavailableSprite;
                    forPurchaseStatus.SetActive(true);
                    availableStatus.SetActive(false);
                    activeStatus.SetActive(false);
                    activeBackground.enabled = false;
                    priceText.text = price.ToString();
                    TweenScale();
                    break;
                case ProductStatus.NotEnoughStars:
                    productImage.sprite = unavailableSprite;
                    forPurchaseStatus.SetActive(true);
                    availableStatus.SetActive(false);
                    activeStatus.SetActive(false);
                    activeBackground.enabled = false;
                    priceText.text = price.ToString();
                    break;
            }
        }

        private void TweenScale()
        {
            LeanTween.scale(forPurchaseStatus, new Vector3(0.5f, 0.5f, 1), 0.8f).setLoopPingPong();
        }

        private void RemoveActiveStatus(ThemeNames selectedTheme)
        {
            if (theme.GetThemeName() != selectedTheme && currentStatus == ProductStatus.Active)
            {
                currentStatus = ProductStatus.AvailableForSelection;
                SetProductStatus(currentStatus);
            }
        }

        private void SetAsActiveStatus()
        {
            spriteBank.SaveTheme(theme.GetThemeName().ToString());
            currentStatus = ProductStatus.Active;
            SetProductStatus(currentStatus);
        }

        // Called from Unity Event
        public void TryPurchase()
        {
            switch (currentStatus)
            {
                case ProductStatus.Active:
                    LeanTween.scale(activeStatus, new Vector3(0.40f, 0.40f, 1), 0.15f).setOnComplete(() =>
                    {
                        LeanTween.scale(activeStatus, new Vector3(0.35f, 0.35f, 1), 0.15f);
                    });
                    break;
                case ProductStatus.AvailableForSelection:
                    SetAsActiveStatus();
                    if (onNewProductSelected != null)
                    {
                        onNewProductSelected(theme.GetThemeName());
                    }
                    break;
                case ProductStatus.AvailableForPurchase:
                    theme.SetAsAvailable();
                    SetAsActiveStatus();
                    starBank.WithdrawStars(price);
                    if (onNewProductSelected != null)
                    {
                        onNewProductSelected(theme.GetThemeName());
                    }
                    break;
                case ProductStatus.NotEnoughStars:
                    Debug.Log("You don't have enough stars!");
                    break;
            }
        }
    }

}

