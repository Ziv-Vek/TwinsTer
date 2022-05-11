using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Twinster.Tutorial
{
    [CreateAssetMenu(fileName = "TextBlock", menuName = "TwinsTer/TextBlock", order = 1)]
    public class TextSO : ScriptableObject {
        [TextArea(2, 6)]
        [SerializeField] string text;

        public string GetText()
        {
            return text;
        }
    }
}

