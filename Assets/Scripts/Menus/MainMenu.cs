using UnityEngine;
using UnityEngine.InputSystem;
using Twinster.Scenes;

namespace Twinster.Menus
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] GameObject settingsCanvas = null;

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
                        Debug.Log("You pressed a button");
                    }
                }

                Debug.Log("you pressed somewhere");
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
            FindObjectOfType<LevelLoader>().LoadGame();
        }


    }
}

