using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Twinster.Core;

namespace Twinster.UI
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI timerText;
        float timerValue = 0;

        private void Awake() {
            timerValue = FindObjectOfType<LevelSettings>().TimerSet;
        }

        private void Start() {
            if (timerValue <= 1)
            {
                timerValue = FindObjectOfType<LevelSettings>().TimerSet;
            }
        }
    
        void Update()
        {
            if(timerValue > 0)
            {
                timerValue -= Time.deltaTime;
            }
            else
            {
                timerValue = 0;
            }

            DistplayTime(timerValue);
        }

        void DistplayTime(float timeToDisplay)
        {
            if(timeToDisplay < 0)
            {
                timeToDisplay = 0;
                ProcessTimeOut();
            }
            else if(timeToDisplay > 0)
            {
                timeToDisplay += 1;
            }

            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        private void ProcessTimeOut()
        {
            //  Add time out sequence
            Debug.Log("ran out of time");
        }
    }
}

