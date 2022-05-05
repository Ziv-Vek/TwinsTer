using UnityEngine;
using UnityEngine.InputSystem;
using Twinster.Scenes;

namespace Twinster.Menus
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] GameObject settingsCanvas = null;
        [SerializeField] GameObject tapText = null;

        private void Start() {
            TweenTapText();
        }
        
        private void Update()
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

