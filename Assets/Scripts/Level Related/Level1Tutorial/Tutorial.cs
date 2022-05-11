using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Twinster.Core;
using TMPro;
using Twinster.UI;
using Twinster.Selection;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace Twinster.Tutorial
{
    public class Tutorial : MonoBehaviour
    {
        // Config:
        [SerializeField] float delayTutorialBackground = 5f;
        [SerializeField] float delayTutorialText = 0.5f;
        [SerializeField] float delayToTapContinueText = 1.5f;
        
        // Cached Ref:
        [SerializeField] GameObject slotMaskPrefab = null;
        [SerializeField] GameObject tutorialBackground, textCanvas, textBackground, continueText, uiFade, uiTimerFade, uiCirclesMask, uiTimerMask = null;
        [SerializeField] TextSO[] textBlocks = null;
        [SerializeField] TMP_Text textArea = null;

        float timer = 0;
        bool isTiming = true;
        bool isTapToContinueEnabled = false;

        GameObject mask1 = null;
        GameObject mask2 = null;

        int stepNum = -1;
        

        private void OnEnable() {
            SlotsSelectionHandler.successfulTwins += NextTutorialStep;
        }

        private void OnDisable() {
            SlotsSelectionHandler.successfulTwins -= NextTutorialStep;
        }


        private void Update() {
            TappingAnywhereListener();
            ShowTutorialTimer();
        }

        void ShowTutorialTimer()
        {
            if (!isTiming) return;

            timer += Time.deltaTime;

            if (timer >= delayTutorialBackground)
            {
                isTiming = false;
                Debug.Log("from timing");
                NextTutorialStep();
            }
        }

        private void NextTutorialStep()
        {
            stepNum++;
            isTapToContinueEnabled = false;
            Debug.Log("stepEnum: " + stepNum);

            switch (stepNum)
            {
                case 0:
                    DisableTimer();
                    tutorialBackground.SetActive(true);
                    textCanvas.SetActive(true);
                    uiTimerFade.SetActive(true);
                    uiFade.SetActive(true);
                    DisableBallParticles();
                    Invoke(nameof(PopulateFirstMask), delayTutorialText);
                    break;
                case 1:
                    DisplayTextBlock(1);
                    mask1.GetComponent<SpriteMask>().enabled = false;
                    mask2.GetComponent<SpriteMask>().enabled = false;
                    uiCirclesMask.SetActive(true);
                    FindObjectOfType<SlotsSelectionHandler>().DisableSelection(true);
                    Invoke(nameof(EnableTapping), delayToTapContinueText);
                    break;
                case 2:
                    continueText.SetActive(false);
                    DisplayTextBlock(2);
                    uiCirclesMask.SetActive(false);
                    uiTimerMask.SetActive(true);
                    Invoke(nameof(EnableTapping), delayToTapContinueText);
                    break;
                case 3:
                    DisplayTextBlock(3);
                    continueText.SetActive(false);
                    uiTimerMask.SetActive(false);
                    PopulateSecondMask();
                    FindObjectOfType<LevelSettings>().enabled = false;
                    FindObjectOfType<SlotsSelectionHandler>().DisableSelection(false);
                    break;
                case 4:
                    DisplayTextBlock(4);
                    DestroyMasks();
                    Invoke(nameof(EnableTapping), delayToTapContinueText);
                    break;
                case 5:
                    FindObjectOfType<LevelSettings>(includeInactive: true).enabled = true;
                    FindObjectOfType<LevelSettings>().ReduceRequiredTwins();
                    break;
            }
        }

        private void DestroyMasks()
        {
            GameObject[] slotMasks = GameObject.FindGameObjectsWithTag("SlotMask");
            foreach (GameObject slotMask in slotMasks)
            {
                Destroy(slotMask);
            }
        }

        private void DisableTimer()
        {
            FindObjectOfType<Timer>().DisableCoundown(true);
        }

        private void PopulateFirstMask()
        {
            Slot[] slots = FindObjectsOfType<Slot>();
            foreach (Slot slot in slots)
            {
                if (slot.TwinEnum == TwinsEnum.TwinA)
                {
                    GameObject mask = Instantiate(slotMaskPrefab, slot.transform.position, Quaternion.identity);
                    mask.transform.SetParent(gameObject.transform);

                    if (mask1 == null)
                    {
                        mask1 = mask;
                    }
                    else
                    {
                        mask2 = mask;
                    }
                }
                else
                {
                    slot.GetComponent<Collider>().enabled = false;
                }
            }
            TextBackgroundTween();
        }

        private void DisableBallParticles()
        {
            Slot[] slots = FindObjectsOfType<Slot>();
            foreach (Slot slot in slots)
            {
                slot.SetBallParticle(null);
            }
        }

        private void PopulateSecondMask()
        {
            Slot[] slots = FindObjectsOfType<Slot>();
            foreach (Slot slot in slots)
            {
                if (slot.TwinEnum == TwinsEnum.TwinB)
                {
                    slot.GetComponent<Collider>().enabled = true;
                    GameObject mask = Instantiate(slotMaskPrefab, slot.transform.position, Quaternion.identity);
                    mask.transform.SetParent(gameObject.transform);

                    if (mask1 == null)
                    {
                        mask1 = mask;
                    }
                    else
                    {
                        mask2 = mask;
                    }
                }
            }
        }

        private void TextBackgroundTween()
        {
            LeanTween.scale(textBackground, new Vector3(1.2f, 1.2f, 1.2f), 0.5f).setEase(LeanTweenType.easeOutQuint).setOnComplete( () => { DisplayTextBlock(0); });
        }

        private void DisplayTextBlock(int textBlockNum)
        {
            textArea.text = textBlocks[textBlockNum].GetText();
        }

        private void ShowContinueText()
        {
            if (!continueText.activeSelf)
            {
                continueText.SetActive(true);
            }

            LeanTween.scale(continueText, new Vector3(1.15f, 1.15f, 1.15f), 0.5f).setDelay(0.2f).setOnComplete( () => 
                LeanTween.scale(continueText, Vector3.one, 0.5f).setDelay(0.2f).setOnComplete(ShowContinueText));
        }

        void EnableTapping()
        {
            isTapToContinueEnabled = true;
            ShowContinueText();
        }

        private void TappingAnywhereListener()
        {
            if (!isTapToContinueEnabled) return;
            
            if (!Touchscreen.current.primaryTouch.press.isPressed) return;

            LeanTween.cancel(continueText);
            NextTutorialStep();
        }

        
    }
}

