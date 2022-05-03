using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Twinster.Core;
using Twinster.Selection;

namespace Twinster.UI
{
    public class TwinsCounterDisplay : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI counterDisplay;

        int twinsRequired = 0;
        bool canReduceMoreTwins = true;

        private void Awake() {
            twinsRequired = FindObjectOfType<LevelSettings>().RequiredNumOfTwins;
        }

        void Start()
        {
            if (twinsRequired == 0)
            {
                twinsRequired = FindObjectOfType<LevelSettings>().RequiredNumOfTwins;
            }

            UpdateCounter();
        }

        private void OnEnable() {
            SlotsSelectionHandler.successfulTwins += ReduceRequiredTwins;
        }

        private void OnDisable() {
            SlotsSelectionHandler.successfulTwins -= ReduceRequiredTwins;
        }

        private void UpdateCounter()
        {
            if (canReduceMoreTwins)
            {
                counterDisplay.text = twinsRequired.ToString();
            }
        }

        public void ReduceRequiredTwins()
        {
            twinsRequired--;
            UpdateCounter();
            if (twinsRequired == 0)
            {
                canReduceMoreTwins = false;
            }
        }
    }
}

