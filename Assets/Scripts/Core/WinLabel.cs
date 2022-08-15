using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Twinster.UI;
using System;
using Twinster.Bank;
using Twinster.Scenes;
using Twinster.Audio;

namespace Twinster.Core
{
    public class WinLabel : MonoBehaviour
    {
        [Space(5)]
        [Header("Config:")]
        [Space(3)]
        [SerializeField] float timerCountdownSpeed = 40f;
        [SerializeField] float secondsToRecieveStar = 15f;
        [SerializeField] float delayCountdown = 0.2f;

        [Space(5)]
        [Header("Cached References:")]
        [Space(3)]
        [SerializeField] TMP_Text timerText;
        [SerializeField] TMP_Text starsText;
        [SerializeField] GameObject littleStarsPrefab, shiningBackground, bigStar, nextButton;

        float remainingTimeToDisplay = 0;
        int starsGainFromTimer = 0;
        bool isCoundownFinished = false;
        LevelLoader levelLoader;

        void Start()
        {
            LeanTween.rotateAround(shiningBackground, Vector3.forward, 360, 30f).setLoopClamp();
            UpdateStarsDisplay();
            remainingTimeToDisplay = FindObjectOfType<Timer>().GetTimeToDisplay();
            starsGainFromTimer = Mathf.FloorToInt(DisplayTime());
            levelLoader = FindObjectOfType<LevelLoader>();
            levelLoader.SaveLevel();
            //levelLoader.TinySauceWinLevel();

            StartCoroutine("ReduceRemainingTime");

        }

        // Called form Unity event (button)
        public void NextLevel()
        {
            if (!isCoundownFinished)
            {
                FindObjectOfType<StarsBank>().DepositStars(starsGainFromTimer);
            }
            if (FindObjectOfType<MusicManager>())
            {
                FindObjectOfType<MusicManager>().UnPauseMusic();
            }
            
            levelLoader.CheckAdDisplay();
        }

        private void UpdateStarsDisplay()
        {
            if (FindObjectOfType<StarsBank>() == null)
            {
                Debug.LogError("No StarBank is found in the scene");
                return;
            }
            starsText.text = FindObjectOfType<StarsBank>().Stars.ToString();
        }

        private float DisplayTime()
        {
            if (remainingTimeToDisplay < 0)
            {
                remainingTimeToDisplay = 0;
            }

            float minutes = Mathf.FloorToInt(remainingTimeToDisplay / 60);
            float seconds = Mathf.FloorToInt(remainingTimeToDisplay % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            return minutes;
        }

        IEnumerator ReduceRemainingTime()
        {
            float seconds = 0;

            yield return new WaitForSeconds(delayCountdown);
            
            while (remainingTimeToDisplay > 0)
            {
                remainingTimeToDisplay -= Time.deltaTime * timerCountdownSpeed;
                seconds += Time.deltaTime * timerCountdownSpeed;
                if (seconds >= secondsToRecieveStar)
                {
                    GainCounterStar();
                    seconds = 0;
                }
                DisplayTime();
                yield return null;
            }

            isCoundownFinished = true;
        }

        private void GainCounterStar()
        {
            GameObject littleStar = Instantiate(littleStarsPrefab, timerText.transform.position, Quaternion.identity);
            littleStar.transform.SetParent(gameObject.transform);
            LeanTween.move(littleStar, bigStar.transform.position, 1f).setOnComplete( () => {
                FindObjectOfType<StarsBank>().DepositStars(1);
                starsGainFromTimer--;
                BigStarTween();
                Destroy(littleStar);
                UpdateStarsDisplay();
                } );
        }

        private void BigStarTween()
        {
            LeanTween.scale(bigStar, new Vector3(0.9f, 0.9f, 1), 0.1f).setOnComplete( () => {
                LeanTween.scale(bigStar, new Vector3(0.8f, 0.8f, 1), 0.1f);
            });
        }
    }
}

