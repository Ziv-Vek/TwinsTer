using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Twinster.Core
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] TwinsEnum twinEnum;
        public TwinsEnum TwinEnum {
            set { twinEnum = value; } 
            get { return twinEnum; }
        }
    }
}

