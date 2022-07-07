using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Twinster.Saving;

namespace Twinster.Sprites
{
    public class ThemesStatus : MonoBehaviour, ISaveable
    {
        private Dictionary<ThemeNames, bool> themesAvailablity = new Dictionary<ThemeNames, bool>();

        public object CaptureState()
        {
            return themesAvailablity;
        }

        public void RestoreState(object state)
        {
            Debug.Log(state);
        }
    }
}