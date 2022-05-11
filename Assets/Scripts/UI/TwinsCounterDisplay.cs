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

        public void ReduceRequiredTwins()
        {
            twinsRequired--;
            UpdateCounter();
            if (twinsRequired == 0)
            {
                canReduceMoreTwins = false;
            }
        }

        public void TweenScale()
        {
            if (LeanTween.isTweening(gameObject))
            {
                Invoke("TweenScale", 0.5f);
            }
            else
            {
                LeanTween.value(gameObject, 100f, 115f, 0.2f).setOnUpdate(ChangeFontSize).setOnComplete( () => {
                    LeanTween.value(gameObject, 115f, 100f, 0.2f).setOnUpdate(ChangeFontSize).setOnComplete( () => LeanTween.cancel(gameObject) );
                    });
            }
        }

        private void UpdateCounter()
        {
            if (canReduceMoreTwins)
            {
                counterDisplay.text = twinsRequired.ToString();
            }
        }

        private void ChangeFontSize(float value)
        {
            counterDisplay.fontSize = value;
        }
    }
}

