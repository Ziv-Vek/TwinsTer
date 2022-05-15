using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Twinster.Scenes;
using Twinster.Saving;

namespace Twinster.Sprites
{
    public class ThemesStore : MonoBehaviour, ISaveable
    {
        public void SelectTheme(string themeName)
        {
            
            bool isAvailable = Resources.Load<ThemeSpritesSO>($"Themes/{themeName}").GetIsAvailable;
            print(isAvailable);
        }
        
        public object CaptureState()
        {
            throw new System.NotImplementedException();
        }

        public void RestoreState(object state)
        {
            throw new System.NotImplementedException();
        }
    }
}
