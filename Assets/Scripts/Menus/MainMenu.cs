using UnityEngine;
using UnityEngine.InputSystem;
using Twinster.Scenes;
using Twinster.Sprites;
using UnityEngine.UI;
using System;

namespace Twinster.Menus
{
    public class MainMenu : MonoBehaviour
    {

        [Space(5)]
        [Header("Background Settings:")]
        [Space(3)]
        [SerializeField] MainMenuBackground[] backgrounds;

        [Space(5)]
        [Header("Cached Ref:")]
        [Space(3)]
        [SerializeField] GameObject settingsCanvas = null;
        [SerializeField] GameObject tapText = null;
        [SerializeField] Image backgroundImage = null;
        [SerializeField] SpriteBank spriteBank = null;

        PlayerInput playerInput;

#if UNITY_STANDALONE_WIN
        bool isPlatformMobile = false;
#elif UNITY_ANDROID
        bool isPlatformMobile = true;
#elif UNITY_IOS
        bool isPlatformMobile = true;
#endif

        private void Awake()
        {
            playerInput = FindObjectOfType<PlayerInput>();
            playerInput.onActionTriggered += PlayerInput_onActionTriggered;
        }

        private void PlayerInput_onActionTriggered(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                
            }
        }

        [System.Serializable]
        public class MainMenuBackground
        {
            public string fontName;
            public string FontName { set { fontName = value; } }

            [SerializeField] ThemeNames themeName;
            public ThemeNames ThemeName { get { return themeName;  } }
            [SerializeField] Sprite themeBackground;
            public Sprite ThemeBackground {  get { return themeBackground;  } }
            [SerializeField] Color32 backgroundColor;
            public Color32 BackgroundColor { get { return backgroundColor;  } }
        }

        private void Start() {
            ShowThemeBackground();
            TweenTapText();
        }

        private void ShowThemeBackground()
        {
            ThemeNames activeThemeName = spriteBank.GetActiveThemeName();

            foreach (MainMenuBackground background in backgrounds)
            {
                if (background.ThemeName == activeThemeName)
                {
                    backgroundImage.sprite = background.ThemeBackground;
                    backgroundImage.color = background.BackgroundColor;
                    return;
                }
            }
        }

        private void Update()
        {
            GetMobileInput();
            if (!isPlatformMobile)
            {
                //GetMobileInput();
            }
            else
            {
                //GetWindowsInput();
            }
        }

        private void GetWindowsInput()
        {
            {
                // Check if player is hitting a button or just taps blank space to play the game.
                Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
                Ray ray = Camera.main.ScreenPointToRay(touchPosition);
                RaycastHit hit;
                bool hasHit = Physics.Raycast(ray, out hit);
                if (!hasHit)
                {
                    LoadGame();
                }
                else
                {
                    bool isHittingButton = hit.collider.gameObject.CompareTag("Button");
                    if (isHittingButton)
                    {
                        // DO nothing?
                    }
                }

                // DO nothing?
            }
        }

        private void GetMobileInput()
        {
            if (Touchscreen.current.primaryTouch.press.isPressed)
            {
                // Check if player is hitting a button or just taps blank space to play the game.
                Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
                Ray ray = Camera.main.ScreenPointToRay(touchPosition);
                RaycastHit hit;
                bool hasHit = Physics.Raycast(ray, out hit);
                if (!hasHit)
                {
                    LoadGame();
                }
                else
                {
                    bool isHittingButton = hit.collider.gameObject.CompareTag("Button");
                    if (isHittingButton)
                    {
                        // DO nothing?
                    }
                }

                // DO nothing?
            }
        }

        public void ShowSettings()
        {
            if (settingsCanvas == null)
            {
                Debug.LogError("Settings canvas is not set!");
                return;
            }
            settingsCanvas.SetActive(true);
            gameObject.SetActive(false);
        }

        public void ShowThemesMenu()
        {
            FindObjectOfType<LevelLoader>().LoadThemesStore();
        }

        private void LoadGame()
        {
            FindObjectOfType<LevelLoader>().StartGame();
        }

        void TweenTapText()
        {
            LeanTween.scale(tapText, new Vector3(1.05f, 1.15f, 1f), 0.6f).setDelay(0.1f).setOnComplete( () => 
                LeanTween.scale(tapText, new Vector3(0.9f, 0.85f, 0.9f), 0.6f).setDelay(0.1f).setOnComplete(TweenTapText));
        }


    }
}

