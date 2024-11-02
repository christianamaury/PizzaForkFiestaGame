using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {get; set;}

    //An array of Sounds;
    public Sounds[] sounds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //Don't Destroy Object when a new scene loads up;
        DontDestroyOnLoad(gameObject);


        //For each sounds component, add a component
        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Play Custom Theme song Here;
        Play("ThemeSong");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Play Sound;
    public void Play(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);

        //If its null, there's no Audio at all;
        if (s == null)
        {
            Debug.Log("No audio under that name");

        }

        //Play available audio;
        s.source.Play();

    }

    //Stop Sound;
    public void Stop(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);

        if(s == null)
        {
            //There's no audio available to stop at the moment;
            Debug.Log("No audio to stop playing at the moment");
            return;
        }

        s.source.Stop();

    }


    //It's sound Playing;
    public bool isPlaying(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            //No audio available;
            return false;
        }

        //Return playing audio at the moment;
        return s.source.isPlaying;
    }
}
