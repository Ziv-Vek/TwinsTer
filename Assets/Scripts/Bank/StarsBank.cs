using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Twinster.Bank
{
    public class StarsBank : MonoBehaviour
    {
        int stars = 0;
        public int Stars { get { return stars; } }

        const string STARS_IN_BANK = "StarsInBank";

        private void Start() {
            if (!PlayerPrefs.HasKey("StarsInBank"))
            {
                stars = 0;
            }
            else
            {
                stars = PlayerPrefs.GetInt(STARS_IN_BANK, 0);
            }
        }

        public void DepositStars(int amount)
        {
            stars += amount;
            PlayerPrefs.SetInt(STARS_IN_BANK, stars);
        }

        public void WithdrawStars(int amount)
        {
            stars -= amount;
            PlayerPrefs.SetInt(STARS_IN_BANK, stars);
        }
    }
}

