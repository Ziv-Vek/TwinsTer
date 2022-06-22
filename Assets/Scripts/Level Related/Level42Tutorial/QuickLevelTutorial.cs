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
    public class QuickLevelTutorial : MonoBehaviour
    {
        // Config:
        [SerializeField] float delayTutorialBackground = 5f;
        [SerializeField] float delayTutorialText = 0.5f;
        [SerializeField] float delayToTapContinueText = 1.5f;

        // Cached Ref:
        [SerializeField] GameObject tutorialBackground, textCanvas, textBackground, continueText, uiFade, uiTimerFade = null;
        [SerializeField] TextSO[] textBlocks = null;
        [SerializeField] TMP_Text textArea = null;

        float timer = 0;
        bool isTiming = true;
        bool isTapToContinueEnabled = false;

        int stepNum = -1;

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
/*                case 1:
                    FindObjectOfType<SlotsSelectionHandler>().DisableSelection(false);
                    continueText.SetActive(false);
                    DisplayTextBlock(1);
                    break;
                case 2:
                    DisplayTextBlock(2);
                    FindObjectOfType<SlotsSelectionHandler>().DisableSelection(true);
                    Invoke(nameof(EnableTapping), delayToTapContinueText);
                    break;
                case 3:
                    DisplayTextBlock(3);
                    Invoke(nameof(EnableTapping), delayToTapContinueText);
                    //uiTimerMask.SetActive(false);
                    break;*/
                case 1:
                    tutorialBackground.SetActive(false);
                    textCanvas.SetActive(false);
                    uiTimerFade.SetActive(false);
                    uiFade.SetActive(false);
                    FindObjectOfType<SlotsSelectionHandler>().DisableSelection(false);
                    EnableSlotsSelection();
                    DisableTimer(false);
                    break;
            }
        }

        private void DisableTimer(bool isToDisable)
        {
            FindObjectOfType<Timer>().DisableCoundown(isToDisable);
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
