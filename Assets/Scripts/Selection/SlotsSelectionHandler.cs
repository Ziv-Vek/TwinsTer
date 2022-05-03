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
        [SerializeField] GameObject slotBackground = null;
        
        Slot previousSlot = null;     // serialized for debugging
        Camera mainCamera;
        bool isSelectionDisabled = false;

        public delegate void SuccessfulTwins();
        public static SuccessfulTwins successfulTwins;

        private void Awake() {
            mainCamera = Camera.main;
        }

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
            if (previousSlot == null)
            {
                previousSlot = selectedSlot;
                
                // Selection VFX
                PaintSlotBackground(previousSlot);
                selectedSlot = null;

                return;
            }

            if (selectedSlot.gameObject == previousSlot.gameObject)
            {
                Debug.Log("You selected the same slot");
                return;
            }

            if (previousSlot.TwinEnum == TwinsEnum.NoTwin || selectedSlot.TwinEnum == TwinsEnum.NoTwin)
            {
                // Remove selection VFX from previous slot
                // Add selection VFX to selected slot
                PaintSlotBackground(selectedSlot);
                previousSlot = selectedSlot;

                return;
            }

            if (previousSlot.TwinEnum != selectedSlot.TwinEnum)
            {
                // Remove selection VFX from previous slot
                // Add selection VFX to selected slot
                PaintSlotBackground(selectedSlot);
                previousSlot = selectedSlot;

                return;
            }

            ProcessSuccessfulTwin(selectedSlot);
        }

        private void ProcessSuccessfulTwin(Slot selectedSlot)
        {
            successfulTwins();
            Destroy(previousSlot.gameObject);
            Destroy(selectedSlot.gameObject);
            RemoveSlotBackground();
            previousSlot = null;
            
        }

        private void PaintSlotBackground(Slot slot)
        {
            slotBackground.transform.SetPositionAndRotation(slot.transform.position, slot.transform.rotation);
            slotBackground.GetComponent<SpriteRenderer>().sprite = slot.GetComponent<SpriteRenderer>().sprite;
        }

        
        private void RemoveSlotBackground()
        {
            slotBackground.GetComponent<SpriteRenderer>().sprite = null;
        }
    }
}
