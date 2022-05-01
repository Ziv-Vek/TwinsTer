using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Twinster.UI;

namespace Twinster.Core
{
    public class WinLabel : MonoBehaviour
    {
        [Space(5)]
        [Header("Config:")]
        [Space(3)]
        [SerializeField] float timerCountdownSpeed = 20f;
        [Space(5)]
        [Header("Cached References:")]
        [Space(3)]
        [SerializeField] TMP_Text timerText;
        [SerializeField] TMP_Text starsText;
        [SerializeField] GameObject littleStarsPrefab, shiningBackground;


        float remainingTimeToDisplay = 0;

        void Start()
        {
            LeanTween.rotateAround(shiningBackground, Vector3.forward, 360, 30f).setLoopClamp();

            remainingTimeToDisplay = FindObjectOfType<Timer>().GetTimeToDisplay();
            DisplayTime();

            StartCoroutine("ReduceRemainingTime");
        }

        private void DisplayTime()
        {
            if (remainingTimeToDisplay < 0)
            {
                remainingTimeToDisplay = 0;
            }

            float minutes = Mathf.FloorToInt(remainingTimeToDisplay / 60);
            float seconds = Mathf.FloorToInt(remainingTimeToDisplay % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        IEnumerator ReduceRemainingTime()
        {
            yield return new WaitForSeconds(2f);
            
            Debug.Log("reduction code started");
            while (remainingTimeToDisplay > 0)
            {
                remainingTimeToDisplay -= Time.deltaTime * timerCountdownSpeed;
                DisplayTime();
                yield return null;
            }
            Debug.Log("countdown finished");
        }



    }
}

