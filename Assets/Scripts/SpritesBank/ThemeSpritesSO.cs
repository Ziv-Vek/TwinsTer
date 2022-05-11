using UnityEngine;

namespace Twinster.Sprites
{
    [CreateAssetMenu(fileName = "NewTheme", menuName = "TwinsTer/New Theme Sprites", order = 0)]
    public class ThemeSpritesSO : ScriptableObject 
    {
        // [HideInInspector]
        //public string fontName;
        
        [Space(3)]
        [SerializeField] ThemeNames theme;
        
        [Space(5)]       
        [Header("Sprites & Images:")]
        [Space(3)]
        [SerializeField] Sprite background = null;
        [SerializeField] Color backgorundColor = Color.white;
        [SerializeField] Sprite[] slotsSprites = null;

        [Space(5)]       
        [Header("Stats:")]
        
        [Space(3)]
        [SerializeField] bool isAvailable = false;
        [SerializeField] int starsCost = 0;
    }
}
