using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Twinster.Selection
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerInput playerInput;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>(); 
        }
    }
}