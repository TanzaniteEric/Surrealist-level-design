using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueDisplayer : MonoBehaviour
{
    public TextBlurb dragonTextBlurb;
    public TextBlurb playerTextBlurb;
    public GameObject dragonTextIndicator;
    public GameObject playerTextIndicator;

    public enum Speaker
    {
        Player,
        Mom,
        Dad,
        Sister, 
        All
    }

    public void ClearDialogue(bool player)
    {
        if (player)
        {
            playerTextBlurb.SetText("");
            playerTextIndicator.SetActive(false);
        }
        else
        {
            dragonTextBlurb.SetText("");
            dragonTextIndicator.SetActive(false);
        }
    }

    public void WriteDialogue(Dialogue dialogue, bool angry = false)
    {
        Speaker source = dialogue.speaker;
        string content = dialogue.line;

        if (source == Speaker.Player)
        {
            playerTextIndicator.SetActive(true);
            dragonTextIndicator.SetActive(false);
        }
        else
        {
            dragonTextIndicator.SetActive(true);
            playerTextIndicator.SetActive(false);
        }
        
        int dragonHead = -1;

        if (source != Speaker.Player)
        {
            switch (source)
            {
                case Speaker.Dad:
                    dragonHead = 1;
                    break;
                case Speaker.Mom:
                    dragonHead = 2;
                    break;
                case Speaker.Sister:
                    dragonHead = 3;
                    break;
                case Speaker.All:
                    dragonHead = 4;
                    break;
                default:
                    dragonHead = -1;
                    break;
            }
        }
        
        if (source == Speaker.Player)
        {
            playerTextBlurb.SetText(content);
        }
        else
        {
            bool inHumanForm = (GameManager.Instance.currentWave.currentState ==
                                   WaveController.WaveState.goodEnding || GameManager.Instance.currentWave.currentState == WaveController.WaveState.prologue);
            string speakerName = "";
            switch (source) {
                case Speaker.Dad:
                    if (!inHumanForm) speakerName = "Head 1";
                    else speakerName = "Father";
                    break;
                case Speaker.Mom:
                    if (!inHumanForm) speakerName = "Head 2";
                    else speakerName = "Mother";
                    break;
                case Speaker.Sister:
                    if (!inHumanForm) speakerName = "Head 3";
                    else speakerName = "Sister";
                    break;
                case Speaker.All:
                    speakerName = "All";
                    break;
            }

            dragonTextBlurb.SetText(speakerName + ": " + content, dragonHead);
        }
    }
}
