using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;


    void Awake()
    {
        //If I'm the only one of myself in scene
        if (instance == null)
        {
            instance = this;
        }
        //If I'm not the original
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        /*
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.spatialBlend = 0.9f;
            s.source.clip = s.clip;


            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }*/
    }

    public void Play(string name, GameObject go)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        s.source = go.AddComponent<AudioSource>();
        s.source.spatialBlend = 0.9f;
        s.source.clip = s.clip;


        s.source.volume = s.volume;
        s.source.pitch = s.pitch;

        s.source.Play();
    }
}


//FindObjectOfType<AudioManager>().Play("xx");
//AudioManager.instance.Play("", gameObject);