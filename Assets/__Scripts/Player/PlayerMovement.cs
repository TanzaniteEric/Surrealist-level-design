using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using FMOD.Studio;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInputHandler inputHandler;
    
    public Spring2D spring;
    
    [FormerlySerializedAs("moveSpeed")] public float fastMoveSpeed;
    public float slowMoveSpeed;

    private float currentMoveSpeed;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    //audio
    private EventInstance playerFootsteps;
    
    void Start()
    {
        currentMoveSpeed = fastMoveSpeed;
        playerFootsteps = AudioManager.instance.CreateEventInstance(FMODevents.instance.walkingAudio);
    }
    
    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        transform.position = spring.position;
    }

    public void SetFast()
    {
        currentMoveSpeed = fastMoveSpeed;
    }

    public void SetSlow()
    {
        currentMoveSpeed = slowMoveSpeed;
    }
    
    private void UpdateMovement()
    {
        Vector2 moveVector = Vector2.zero;
        
        if (inputHandler.playerInputState.moveUp)
        {
            // Move up
            moveVector += Vector2.up;
        }
        if (inputHandler.playerInputState.moveDown)
        {
            // Move down
            moveVector += Vector2.down;
        }
        if (inputHandler.playerInputState.moveLeft)
        {
            // Move left
            moveVector += Vector2.left;
        }
        if (inputHandler.playerInputState.moveRight)
        {
            // Move right
            moveVector += Vector2.right;
        }
        
        Vector2 desiredPos = spring.equilibriumPosition + (moveVector.normalized * currentMoveSpeed * Time.deltaTime);
        // Clamp desiredPos to be within the bounds of minX, maxX, minY, maxY
        desiredPos.x = Mathf.Clamp(desiredPos.x, minX, maxX);
        desiredPos.y = Mathf.Clamp(desiredPos.y, minY, maxY);
        spring.equilibriumPosition = desiredPos;
    }

    private void startWalking()
    {
        PLAYBACK_STATE playbackState;
        playerFootsteps.getPlaybackState(out playbackState);
        if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
        {
            playerFootsteps.start();
        }
    }
    
    private void stopWalking()
    {
        PLAYBACK_STATE playbackState;
        playerFootsteps.getPlaybackState(out playbackState);
        if (@playbackState.Equals(PLAYBACK_STATE.STOPPED))
        {
            playerFootsteps.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }
}
