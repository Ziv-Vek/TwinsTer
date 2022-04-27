using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Twinster.Sprites
{
    public class SpriteBank : MonoBehaviour
    {
        [SerializeField] List<Sprite> bank = new List<Sprite>();
        [SerializeField] Sprite backgroundBank = null;

        public Sprite GetSprite()
        {
            int randNum = Random.Range(0, bank.Count);
            Sprite sprite = bank[randNum];
            bank.RemoveAt(randNum);
            return sprite;
        }

        public int GetBankLengh()
        {
            return bank.Count;
        }

        public Sprite GetThemeBackground()
        {
            return backgroundBank;
        }
        
    }
}

