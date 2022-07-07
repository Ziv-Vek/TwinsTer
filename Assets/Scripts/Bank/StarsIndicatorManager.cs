using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace Twinster.Bank
{
    public class StarsIndicatorManager : MonoBehaviour
    {
        [Space(3)]
        [SerializeField] int maxViewsBeforeAddEnabled = 3;
        [SerializeField] StarsBank starBank;

        [Space(5)]
        [Header("Cached Ref:")]
        [SerializeField] TextMeshProUGUI starsText;

        // Stats:
        int addViewsCounter;

        private void OnEnable()
        {
            starBank.onBankDeposit += UpdateIndicator;
            starBank.onBankWithdraw += UpdateIndicator;
        }

        private void OnDisable()
        {
            starBank.onBankDeposit -= UpdateIndicator;
            starBank.onBankWithdraw -= UpdateIndicator;
        }

        void Start()
        {
            ReduceViewsCounter();
            UpdateIndicator();
        }

        private void ReduceViewsCounter()
        {
            addViewsCounter = PlayerPrefs.GetInt("addStarsViewCount", maxViewsBeforeAddEnabled);

            if (addViewsCounter - 1 <= 0)
            {
                addViewsCounter = maxViewsBeforeAddEnabled;
            }
            else
            {
                addViewsCounter--;
            }

            PlayerPrefs.SetInt("addStarsViewCount", addViewsCounter);
        }

        void UpdateIndicator()
        {
            starsText.text = starBank.Stars.ToString();
        }
        
        public void AddStars()
        {
            if (addViewsCounter == maxViewsBeforeAddEnabled)
            {
                //
            }
        }

    }

}

