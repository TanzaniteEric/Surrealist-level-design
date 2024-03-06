using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DialogueSequence", menuName = "ScriptableObjects/Dialogue")]
public class DialogueSeq: ScriptableObject
{

    //play this before shooting wave
    public Dialogue[] dialogueBeforeWave;

    //play one of below based on the first projectile hitting the dragon in this story unit
    public Dialogue[] playerReplyRed;
    public Dialogue[] playerReplyBlue;

}

[System.Serializable]
public class Dialogue
{
    public DialogueDisplayer.Speaker speaker;
    [TextArea(3,10)]
    public string line;
}
