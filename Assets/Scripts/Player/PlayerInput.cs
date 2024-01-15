using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

    public class PlayerInputs : MonoBehaviour
    {
        public PlayerInput playerInput;

        void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
        }
    }

