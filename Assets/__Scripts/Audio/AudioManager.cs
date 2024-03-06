using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    private EventInstance musicEventInstance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Audio Manager");
        }

        instance = this;
    }

    private void Start()
    {
        InitializeMusic(FMODevents.instance.levelBGM);
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager. PlayOneShot(sound, worldPos);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }

    private void InitializeMusic(EventReference musicEventReference)
    {
        musicEventInstance = CreateEventInstance(musicEventReference);
        musicEventInstance.start();
    }
}
