using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class FMODevents : MonoBehaviour
{
    [field: Header("BGM")]
    [field: SerializeField] public EventReference levelBGM { get; private set; }
    
    [field: Header("Walking SFX")]
    [field: SerializeField] public EventReference walkingAudio { get; private set; }
    
    [field: Header("Bullet SFX")]
    [field: SerializeField] public EventReference angryAudio { get; private set; }
    [field: SerializeField] public EventReference angryTraveling { get; private set; }
    [field: SerializeField] public EventReference calmAudio { get; private set; }
    [field: SerializeField] public EventReference calmTraveling { get; private set; }
    
    public static FMODevents instance { get; private set; }
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Audio Manager");
        }

        instance = this;
    }
}
