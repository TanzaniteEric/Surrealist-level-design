using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController
{
    public bool dragonHitThisWave;
    public bool hitWasBlue;
    public DialogueSeq dialogueSeq;
    public ProjectileWave projectileWave;

    public enum WaveState
    {
        preWave,
        duringWave,
        playerResponse,
        goodEnding,
        badEnding,
        prologue
    }

    public WaveState currentState;
    private int dialogueIndex;
    private Dialogue[] currentDialogues;

    private float timer = 0;
    public float waveDuration;
    
    public WaveController(DialogueSeq dialogueSeq, ProjectileWave projectileWave, float waveDuration)
    {
        this.dialogueSeq = dialogueSeq;
        this.projectileWave = projectileWave;
        this.waveDuration = waveDuration;
        dragonHitThisWave = false;
        
        currentState = WaveState.preWave;
        currentDialogues = dialogueSeq.dialogueBeforeWave;
    }

    public void UpdateTimer(float deltaTime)
    {
        if (currentState != WaveState.duringWave) return;
        timer += deltaTime;

        if (timer >= waveDuration)
        {
            WaveEnd();
        }
    }

    private void WaveEnd()
    {
        int dragonHealthChange = GameManager.Instance.enemyHealth.WaveEndHealthDiff();
        bool sumWasBlue = (dragonHealthChange > 0);

        if (dragonHealthChange != 0)
        {
            GameManager.Instance.SwitchToDialogue();
            PlayerDialogueStart(sumWasBlue);
            // Have game manager create a new wave controller and start over
        }
        else
        {
            // Reset this wave
            ShootAgain();
        }
        
    }

    private void ShootAgain()
    {
        dragonHitThisWave = false;
        
        currentState = WaveState.duringWave;
        timer = 0;
        GameManager.Instance.projectileSpawner.SpawnWave(projectileWave);
    }

    public void Step()
    {
        // Step doesn't do anything during a wave
        if (currentState == WaveState.duringWave) return;
        
        // Write the next sentence -- if a sentence was written, end here
        if (currentState == WaveState.badEnding)
        {
            if (dialogueIndex == 1)
            {
                GameManager.Instance.dialogueDisplayer.dragonTextBlurb.head1.SetActive(false);
            }
            else if (dialogueIndex == 2)
            {
                GameManager.Instance.dialogueDisplayer.dragonTextBlurb.head2.SetActive(false);
            }
            else if (dialogueIndex == 3)
            {
                GameManager.Instance.dialogueDisplayer.dragonTextBlurb.head3.SetActive(false);
            }
        }
        
        if (WriteNextSentence()) return;
        
        // Otherwise, move to the next state of the wave
        // (and clear the text box)
        
        switch (currentState)
        {
            case WaveState.preWave:
                // Tell game manager to shoot this wave and resume gameplay
                currentState = WaveState.duringWave;
                GameManager.Instance.dialogueDisplayer.ClearDialogue(false);
                GameManager.Instance.dialogueDisplayer.ClearDialogue(true);
                GameManager.Instance.SwitchToGameplay();
                GameManager.Instance.projectileSpawner.SpawnWave(projectileWave);
                break;
            case WaveState.playerResponse:
                // Tell game manager to create the next wave
                GameManager.Instance.dialogueDisplayer.ClearDialogue(false);
                GameManager.Instance.dialogueDisplayer.ClearDialogue(true);
                GameManager.Instance.GetNewWave();
                break;
            case WaveState.badEnding:
                GameManager.Instance.BadEndingAnimation();
                break;
            case WaveState.goodEnding:
                GameManager.Instance.GoodEndingAnimation();
                break;
            case WaveState.prologue:
                GameManager.Instance.dialogueDisplayer.ClearDialogue(false);
                GameManager.Instance.dialogueDisplayer.ClearDialogue(true);
                GameManager.Instance.PrologueAnimation();
                GameManager.Instance.GetNewWave();
                break;
                
            default:
                Debug.Log("Step shouldn't be called when in this state");
                break;
        }

    }

    /// <summary>
    /// Write the next sentence to the text box
    /// </summary>
    /// <returns> True if there was another sentence to write, false otherwise </returns>
    private bool WriteNextSentence()
    {
        if (dialogueIndex >= currentDialogues.Length) return false;
        
        GameManager.Instance.dialogueDisplayer.WriteDialogue(currentDialogues[dialogueIndex]);
        dialogueIndex++;
        return true;
    }

    public void PlayerDialogueStart(bool blue)
    {
        dialogueIndex = 0;
        hitWasBlue = true;
        currentState = WaveState.playerResponse;

        if (blue)
        {
            currentDialogues = dialogueSeq.playerReplyBlue;
        }
        else
        {
            currentDialogues = dialogueSeq.playerReplyRed;
        }
        
        Step();
    }
    
}
