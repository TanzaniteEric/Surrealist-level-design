using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;

    public struct PlayerInputState
    {
        public bool moveUp;
        public bool moveDown;
        public bool moveLeft;
        public bool moveRight;

        public bool grab;
    }
    
    public PlayerInputState playerInputState;
    
    // Start is called before the first frame update
    void Start()
    {
        playerInput = new PlayerInput();
        
        playerInput.Gameplay.Up.started += ctx => playerInputState.moveUp = true;
        playerInput.Gameplay.Up.canceled += ctx => playerInputState.moveUp = false;
        
        playerInput.Gameplay.Down.started += ctx => playerInputState.moveDown = true;
        playerInput.Gameplay.Down.canceled += ctx => playerInputState.moveDown = false;
        
        playerInput.Gameplay.Left.started += ctx => playerInputState.moveLeft = true;
        playerInput.Gameplay.Left.canceled += ctx => playerInputState.moveLeft = false;
        
        playerInput.Gameplay.Right.started += ctx => playerInputState.moveRight = true;
        playerInput.Gameplay.Right.canceled += ctx => playerInputState.moveRight = false;
        
        playerInput.Gameplay.Grab.started += ctx => playerInputState.grab = true;
        playerInput.Gameplay.Grab.canceled += ctx => playerInputState.grab = false;

        playerInput.Gameplay.Continue.performed += ctx => ContinueDialogue();
        
        playerInput.Gameplay.Enable();

    }

    void ContinueDialogue()
    {
        GameManager.Instance.StepDialogue();
    }
    
    
    
}
