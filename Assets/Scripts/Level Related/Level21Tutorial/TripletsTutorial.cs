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
    public class TripletsTutorial : MonoBehaviour
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
        GameObject mask3 = null;

        int stepNum = -1;


        private void OnEnable()
        {
            SlotsSelectionHandler.successfulTriplets += NextTutorialStep;
        }

        private void OnDisable()
        {
            SlotsSelectionHandler.successfulTriplets -= NextTutorialStep;
        }


        private void Update()
        {
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
                    DisableTimer(true);
                    tutorialBackground.SetActive(true);
                    textCanvas.SetActive(true);
                    uiTimerFade.SetActive(true);
                    uiFade.SetActive(true);
                    FindObjectOfType<SlotsSelectionHandler>().DisableSelection(true);
                    TextBackgroundTween();
                    Invoke(nameof(EnableTapping), delayToTapContinueText);
                    break;
                case 1:
                    FindObjectOfType<SlotsSelectionHandler>().DisableSelection(false);
                    continueText.SetActive(false);
                    DisplayTextBlock(1);
                    DisableBallParticles();
                    Invoke(nameof(PopulateFirstMask), delayTutorialText);
                    break;
                case 2:
                    DisplayTextBlock(2);
                    FindObjectOfType<SlotsSelectionHandler>().DisableSelection(true);
                    uiCirclesMask.SetActive(true);
                    mask1.GetComponent<SpriteMask>().enabled = false;
                    mask2.GetComponent<SpriteMask>().enabled = false;
                    mask3.GetComponent<SpriteMask>().enabled = false;
                    Invoke(nameof(EnableTapping), delayToTapContinueText);
                    break;
                case 3:
                    DisplayTextBlock(3);
                    Invoke(nameof(EnableTapping), delayToTapContinueText);
                    //uiTimerMask.SetActive(false);
                    break;
                case 4:
                    tutorialBackground.SetActive(false);
                    textCanvas.SetActive(false);
                    uiTimerFade.SetActive(false);
                    uiFade.SetActive(false);
                    FindObjectOfType<SlotsSelectionHandler>().DisableSelection(false);
                    DestroyMasks();
                    EnableSlotsSelection();
                    DisableTimer(false);
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

        private void DisableTimer(bool isToDisable)
        {
            FindObjectOfType<Timer>().DisableCoundown(isToDisable);
        }

        private void PopulateFirstMask()
        {
            Slot[] slots = FindObjectsOfType<Slot>();
            foreach (Slot slot in slots)
            {
                if (slot.TripletEnum == TripletsEnum.TripletA)
                {
                    GameObject mask = Instantiate(slotMaskPrefab, slot.transform.position, Quaternion.identity);
                    mask.transform.SetParent(gameObject.transform);

                    if (mask1 == null)
                    {
                        mask1 = mask;
                    }
                    else if (mask2 == null)
                    {
                        mask2 = mask;
                    }
                    else
                    {
                        mask3 = mask;
                    }
                }
                else
                {
                    slot.GetComponent<Collider>().enabled = false;
                }
            }
        }

        private void DisableBallParticles()
        {
            Slot[] slots = FindObjectsOfType<Slot>();
            foreach (Slot slot in slots)
            {
                slot.SetBallParticle(null);
            }
        }

        private void TextBackgroundTween()
        {
            LeanTween.scale(textBackground, new Vector3(1.2f, 1.2f, 1.2f), 0.5f).setEase(LeanTweenType.easeOutQuint).setOnComplete(() => { DisplayTextBlock(0); });
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

            LeanTween.scale(continueText, new Vector3(1.15f, 1.15f, 1.15f), 0.5f).setDelay(0.2f).setOnComplete(() =>
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
        private void EnableSlotsSelection()
        {
            Slot[] slots = FindObjectsOfType<Slot>();
            foreach (Slot slot in slots)
            {
                if (slot.TripletEnum != TripletsEnum.TripletA)
                {
                    slot.GetComponent<Collider>().enabled = true;
                }
            }
        }
    }
}


