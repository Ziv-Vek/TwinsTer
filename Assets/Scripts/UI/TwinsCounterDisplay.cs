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
        [SerializeField] TextMeshProUGUI twinsCounterDisplay;
        [SerializeField] TextMeshProUGUI tripletsCounterDisplay;
        [SerializeField] GameObject blueMarble1;
        [SerializeField] GameObject blueMarble2;
        [SerializeField] GameObject redMarbles;

        int twinsRequired = 0;
        int tripletsRequired = 0;
        bool canReduceMoreTwins = true;
        bool canReduceMoreTriplets = true;

        private void Awake() {
            twinsRequired = FindObjectOfType<LevelSettings>().RequiredNumOfTwins;
            tripletsRequired = FindObjectOfType<LevelSettings>().RequiredNumOfTriplets;
        }

        void Start()
        {
            twinsRequired = FindObjectOfType<LevelSettings>().RequiredNumOfTwins;
            tripletsRequired = FindObjectOfType<LevelSettings>().RequiredNumOfTriplets;

            if (twinsRequired == 0)
            {
                blueMarble1.SetActive(false);
                blueMarble2.SetActive(false);
                twinsCounterDisplay.enabled = false;
                canReduceMoreTwins = false;
            }
            if (tripletsRequired == 0)
            {
                redMarbles.SetActive(false);
                tripletsCounterDisplay.enabled = false;
                canReduceMoreTriplets = false;
            }

            UpdateCounter();
        }

        private void OnEnable() {
            SlotsSelectionHandler.successfulTwins += ReduceRequiredTwins;
            SlotsSelectionHandler.successfulTriplets += ReduceRequiredTriplets;
        }

        private void OnDisable() {
            SlotsSelectionHandler.successfulTwins -= ReduceRequiredTwins;
            SlotsSelectionHandler.successfulTriplets -= ReduceRequiredTriplets;
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

        private void ReduceRequiredTriplets()
        {
            tripletsRequired--;
            UpdateCounter();
            if (tripletsRequired == 0)
            {
                canReduceMoreTriplets = false;
            }
        }

        public void TweenScaleTwin()
        {
            if (LeanTween.isTweening(twinsCounterDisplay.gameObject))
            {
                Invoke("TweenScale", 0.5f);
            }
            else
            {
                LeanTween.value(twinsCounterDisplay.gameObject, 100f, 115f, 0.2f).setOnUpdate(ChangeTwinsFontSize).setOnComplete( () => {
                    LeanTween.value(twinsCounterDisplay.gameObject, 115f, 100f, 0.2f).setOnUpdate(ChangeTwinsFontSize).setOnComplete( () => LeanTween.cancel(twinsCounterDisplay.gameObject) );
                    });
            }
        }

        public void TweenScaleTriplet()
        {
            if (LeanTween.isTweening(tripletsCounterDisplay.gameObject))
            {
                Invoke("TweenScale", 0.5f);
            }
            else
            {
                LeanTween.value(tripletsCounterDisplay.gameObject, 100f, 115f, 0.2f).setOnUpdate(ChangeTripletsFontSize).setOnComplete( () => {
                    LeanTween.value(tripletsCounterDisplay.gameObject, 115f, 100f, 0.2f).setOnUpdate(ChangeTripletsFontSize).setOnComplete( () => LeanTween.cancel(tripletsCounterDisplay.gameObject) );
                    });
            }
        }

        private void UpdateCounter()
        {
            if (canReduceMoreTwins)
            {
                twinsCounterDisplay.text = twinsRequired.ToString();
                TweenScaleTwin();
            }

            if (canReduceMoreTriplets)
            {
                tripletsCounterDisplay.text = tripletsRequired.ToString();
                TweenScaleTriplet();
            }
        }

        private void ChangeTwinsFontSize(float value)
        {
            twinsCounterDisplay.fontSize = value;
        }

        private void ChangeTripletsFontSize(float value)
        {
            tripletsCounterDisplay.fontSize = value;
        }
    }
}

