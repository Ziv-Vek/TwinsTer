using System;
using System.Collections;
using System.Collections.Generic;
using Twinster.Selection;
using Twinster.UI;
using UnityEngine;
using Twinster.Bank;

namespace Twinster.Core
{
    public class LevelSettings : MonoBehaviour
    {
        [Space(3)]
        [Header("Level Settings:")]
        [Space(3)]
        [SerializeField] int numberOfSingles = 0;
        public int NumberOfSingles { get { return numberOfSingles; } }
        [Tooltip("The number of twins that will appear in the level")]
        [SerializeField] int numberOfTwinsPopulated = 0;
        public int NumberOfTwinsPopulated { get { return numberOfTwinsPopulated; } }
        [Tooltip("The number of twins player needs to find in order to win the level")]
        [SerializeField] int requiredNumOfTwins = 0;
        public int RequiredNumOfTwins { get { return requiredNumOfTwins; } }
        [Tooltip("Time for timer, in seconds")]
        [SerializeField] float timerSet = 90;
        public float TimerSet { get { return timerSet; } }

        
        [Space(5)]
        [Header("Cached References:")]
        [Space(3)]
        [SerializeField] GameObject winLabel;
        [SerializeField] GameObject loseLabel;

        int requiredTwinsCoundown = 1;

        public delegate void EventLevelComplete();
        public static EventLevelComplete eventLevelComplete;

        public delegate void SuccessfulTwins();
        public static SuccessfulTwins successfulTwins;

        public delegate void EventLevelLost();
        public static EventLevelLost eventLevelLost;

        private void Start() {
            if (requiredNumOfTwins > numberOfTwinsPopulated)
            {
                Debug.LogError("Number of twins required is higher then number of twins. Check Level Settings");
            }

            requiredTwinsCoundown = requiredNumOfTwins;
        }

        private void OnEnable() {
            SlotsSelectionHandler.successfulTwins += ReduceRequiredTwins;
        }

        private void OnDisable() {
            SlotsSelectionHandler.successfulTwins -= ReduceRequiredTwins;
        }

        public void ProcessLoseCondition()
        {
            eventLevelLost();
            
            FindObjectOfType<SlotsSelectionHandler>().DisableSelection(true);
            FindObjectOfType<Timer>().DisableCoundown(true);
            loseLabel.SetActive(true);
        }

        public void ReduceRequiredTwins()
        {
            requiredTwinsCoundown--;
            if (requiredTwinsCoundown <= 0)
            {
                ProcessWinCondition();
            }
        }

        private void ProcessWinCondition()
        {
            eventLevelComplete();

            if (winLabel == null)
            {
                Debug.LogError("Win Label was not set!");
                return;
            }

            FindObjectOfType<SlotsSelectionHandler>().DisableSelection(true);
            FindObjectOfType<Timer>().DisableCoundown(true);
            FindObjectOfType<StarsBank>().DepositStars(requiredNumOfTwins);
            winLabel.SetActive(true);
        }
    }
}

