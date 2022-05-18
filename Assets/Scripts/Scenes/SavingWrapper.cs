using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Twinster.Saving;

namespace Twinster.Scenes
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }
        
        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

    }

}
