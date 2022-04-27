using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        [SerializeField] int numberOfTwins = 0;
        public int NumberOfTwins { get { return numberOfSingles; } }
        [Tooltip("The number of twins player needs to find in order to win the level")]
        [SerializeField] int requiredNumOfTwins = 0;
        public int RequiredNumOfTwins { get { return requiredNumOfTwins; } }
        [Tooltip("Time for timer, in seconds")]
        [SerializeField] float timerSet = 90;
        public float TimerSet { get { return timerSet; } }

        
        [Space(5)]
        [Header("Cached References:")]
        [Space(3)]
        [SerializeField] Canvas winCanvas;
        [SerializeField] Canvas loseCanvas;

        private void Start() {
            if (requiredNumOfTwins > NumberOfTwins)
            {
                Debug.LogError("Number of twins required is higher then number of twins. Check Level Settings");
            }
        }

        public void ReduceRequiredTwins()
        {
            requiredNumOfTwins--;
            if (requiredNumOfTwins <= 0)
            {
                ProcessWinCondition();
            }
        }

        private void ProcessWinCondition()
        {
            
        }
    }
}

