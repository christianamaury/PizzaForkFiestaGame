using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

 //To show on our Editor;
    [System.Serializable]
    public class Sounds
    {
        public AudioClip clip;

        //Creating a Slider effect;
        [Range(0f, 0.1f)]

        //Creating a Slider Volume;
        public float volume;
        [Range(.1f, 3.0f)]

        public float pitch;

        public string name;

        public bool loop;

        //Hide it in the Inspector section;
        [HideInInspector]
        public AudioSource source;
}

