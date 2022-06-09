using System.Collections;
using System.Collections.Generic;
using Twinster.Saving;
using UnityEngine;

namespace Twinster.Bank
{
    public class StarsBank : MonoBehaviour, ISaveable
    {
        [SerializeField] int stars = 0;     //serialized for debugging
        public int Stars { get { return stars; } }

        public void DepositStars(int amount)
        {
            stars += amount;
            CaptureState();
        }

        public void WithdrawStars(int amount)
        {
            stars -= amount;
            CaptureState();
        }

        public object CaptureState()
        {
            return stars;
        }

        public void RestoreState(object state)
        {
            stars = (int)state;
        }
    }
}

