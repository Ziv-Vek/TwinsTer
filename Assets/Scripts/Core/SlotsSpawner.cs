using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Random = UnityEngine.Random;
using Twinster.Grid;
using Twinster.Sprites;
using System;
using UnityEngine.UI;

namespace Twinster.Core
{
    /// Responsible for instantiating slots (in a given coordinates and images) and setup the parameters for each instantiated slot.
    public class SlotsSpawner : MonoBehaviour
    {
        int numberOfSingles = 0;
        int numberOfTwins = 0;
        [SerializeField] int fixedZPos = 4;
        [SerializeField] Image themeBackgroundImage = null;
        [SerializeField] GameObject slotsParent;        

        public GameObject slotPrefab;
        GridManager gridManager;
        SpriteBank spriteBank;
        TwinsEnum twinsEnum;
        List<GameObject> slotsToPopulate = new List<GameObject>();

        private void Awake() {
            gridManager = FindObjectOfType<GridManager>();
            spriteBank = FindObjectOfType<SpriteBank>();
            numberOfSingles = FindObjectOfType<LevelSettings>().NumberOfSingles;
            numberOfTwins = FindObjectOfType<LevelSettings>().NumberOfTwinsPopulated;
            twinsEnum = 0;
        }

        private void Start()
        {
            if (numberOfSingles == 0)
            {
                numberOfSingles = FindObjectOfType<LevelSettings>().NumberOfSingles;
            }
            if (numberOfTwins == 0)
            {
                numberOfTwins = FindObjectOfType<LevelSettings>().NumberOfTwinsPopulated;
            }

            //CheckForErrorsInLevelConfigurations();
            PopulateBackground();
            PopulateSingleSlots();
            PopulateTwins();

            // randomly pick a sprite from the bank and place instaniate it
            // int randomNum = Random.Range(0, spritesBank.Count);
        }

        // private void CheckForErrorsInLevelConfigurations()
        // {
        //     if ((numberOfSingles + numberOfTwins) > spriteBank.GetBankLengh())
        //     {
        //         Debug.LogError("The number of single slots and twin slots is larger than the bank size");
        //     }
        // }

        private void PopulateBackground()
        {
            themeBackgroundImage.sprite = spriteBank.GetThemeBackground();
        }

        private void PopulateSingleSlots()
        {
            for (int i = 0 ; i < numberOfSingles ; i++)
            {
                GameObject slot = InstantiateSlot(fixedZPos);
                Sprite sprite = spriteBank.GetSprite();
                slot.GetComponent<SpriteRenderer>().sprite = sprite;
                // Sets the enum to mark single (no twin)
                AttachTwinMarkEnum(slot);
            }
        }

        private void PopulateTwins()
        {
            // randomly select an x,y coordinates inside the screen and instantiate
            for (int i = 0; i < numberOfTwins; i++)
            {
                // Instantiate the slot gameobject, without image
                GameObject slot1 = InstantiateSlot(i);
                GameObject slot2 = InstantiateSlot(i);
                
                // Sets the enum to mark twins
                twinsEnum++;
                AttachTwinMarkEnum(slot1);
                AttachTwinMarkEnum(slot2);

                // Attach a random sprite to the slot (fot the twin slots)
                Sprite sprite = spriteBank.GetSprite();
                slot1.GetComponent<SpriteRenderer>().sprite = sprite;
                slot2.GetComponent<SpriteRenderer>().sprite = sprite;
            }
        }

        private GameObject InstantiateSlot(int zPos)
        {
            Vector3 coordinates = gridManager.GetPopulationCoordinate(zPos);
            GameObject slot = Instantiate(slotPrefab, coordinates, Quaternion.identity);
            slot.transform.SetParent(slotsParent.transform);
            TweenSlotScale(slot);
            return slot;
        }

        private void TweenSlotScale(GameObject slot)
        {
            LeanTween.scale(slot, new Vector3(0.5f, 0.5f, 1), 0.5f).setEase(LeanTweenType.easeOutQuint);
        }

        private void AttachTwinMarkEnum (GameObject slot)
        {
            slot.GetComponent<Slot>().TwinEnum = this.twinsEnum;
        }
    }
}

