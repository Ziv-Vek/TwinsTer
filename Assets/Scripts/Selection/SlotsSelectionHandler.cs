using System;
using System.Collections;
using System.Collections.Generic;
using Twinster.Core;
using UnityEngine;
using UnityEngine.InputSystem;
using Twinster.UI;

namespace Twinster.Selection
{
    public class SlotsSelectionHandler : MonoBehaviour
    {
        [SerializeField] GameObject previousSlotBackground = null;
        [SerializeField] GameObject previousPreviousSlotBackground = null;
        
        [SerializeField] Slot currentSlot = null;
        [SerializeField] Slot previousSlot = null;
        [SerializeField] Slot previousPreviousSlot = null;
        Camera mainCamera;
        bool isSelectionDisabled = false;

        public delegate void SuccessfulTwins();
        public static SuccessfulTwins successfulTwins;
        
        public delegate void SuccessfulTriplets();
        public static SuccessfulTriplets successfulTriplets;

        private void Awake() {
            mainCamera = Camera.main;
        }

        // private void Start() {
        //     selectionsArray = {0, 0, 0};
        // }

        void Update()
        {
            if (isSelectionDisabled) return;
            if (!Touchscreen.current.primaryTouch.press.isPressed) return;
            
            GetSelectionInput();
        }

        public void DisableSelection(bool disableSelection)
        {
            isSelectionDisabled = disableSelection;
        }

        private void GetSelectionInput()
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

            RaycastHit hit;
            bool hasHit = Physics.Raycast(worldPosition, mainCamera.transform.forward, out hit, 40);

            if (hasHit && hit.collider.CompareTag("Slot"))
            {
                if (hit.collider.GetComponent<Slot>() == null)
                {
                    Debug.LogError("The hitted slot object does not have a Slot.cs");
                    return;
                }

                Slot selectedSlot = hit.collider.GetComponent<Slot>();
                ProcessSelection(selectedSlot);
            }
        }

        private void ProcessSelection(Slot selectedSlot)
        {
            if (currentSlot == null)
            {
                currentSlot = selectedSlot;
                PaintSlotBackground(currentSlot, previousSlotBackground);
                return;
            }

            if (currentSlot.gameObject == selectedSlot.gameObject)
            {
                return;
            }

            previousPreviousSlot = previousSlot;
            previousSlot = currentSlot;
            currentSlot = selectedSlot;

            if (currentSlot.TwinEnum != TwinsEnum.NoTwin)
            {
                CheckForTwins();
                return;
            }
            else if (currentSlot.TripletEnum != TripletsEnum.NoTriplet)
            {
                CheckForTriplets();
                return;
            }
            else if (currentSlot.TwinEnum == TwinsEnum.NoTwin && currentSlot.TripletEnum == TripletsEnum.NoTriplet)
            {
                ResetSlots();
                return;
            }
        }

        private void ResetSlots()
        {
            RemoveSlotBackground(previousPreviousSlotBackground);
            previousSlot = null;
            previousPreviousSlot = null;
            PaintSlotBackground(currentSlot, previousSlotBackground);
        }

        private void CheckForTwins()
        {
            if (previousSlot == null)
            {
                PaintSlotBackground(currentSlot, previousSlotBackground);
                return;
            }

            if (currentSlot.TwinEnum == previousSlot.TwinEnum)
            {
                ProcessSuccessfulTwin();
                return;
            }

            ResetSlots();
        }

        private void CheckForTriplets()
        {
            if (previousSlot == null)
            {
                PaintSlotBackground(currentSlot, previousSlotBackground);
                return;
            }

            if (previousPreviousSlot == null)
            {
                if (currentSlot.TripletEnum == previousSlot.TripletEnum)
                {
                    PaintSlotBackground(previousSlot, previousPreviousSlotBackground);
                    PaintSlotBackground(currentSlot, previousSlotBackground);
                    return;
                }
                else
                {
                    ResetSlots();
                    return;
                }
            }

            if (currentSlot.TripletEnum == previousSlot.TripletEnum && currentSlot.TripletEnum == previousPreviousSlot.TripletEnum)
            {
                ProcessSuccessfulTriplet();
                return;
            }
            
            ResetSlots();
        }

        private void ProcessSuccessfulTwin()
        {
            successfulTwins();
            previousSlot.DisappearSlot();
            currentSlot.DisappearSlot();
            RemoveSlotBackground(previousSlotBackground);
            RemoveSlotBackground(previousPreviousSlotBackground);
            currentSlot = null;
            previousSlot = null;
            previousPreviousSlot = null;
        }

        private void ProcessSuccessfulTriplet()
        {
            successfulTriplets();
            previousPreviousSlot.DisappearSlot();
            previousSlot.DisappearSlot();
            currentSlot.DisappearSlot();
            RemoveSlotBackground(previousSlotBackground);
            RemoveSlotBackground(previousPreviousSlotBackground);
            currentSlot = null;
            previousSlot = null;
            previousPreviousSlot = null;
        }

        private void PaintSlotBackground(Slot slot, GameObject slotBackground)
        {
            slotBackground.transform.SetPositionAndRotation(slot.transform.position, slot.transform.rotation);
            slotBackground.GetComponent<SpriteRenderer>().sprite = slot.GetComponent<SpriteRenderer>().sprite;
        }
        
        private void RemoveSlotBackground(GameObject slotBackground)
        {
            slotBackground.GetComponent<SpriteRenderer>().sprite = null;
        }
    }
}
