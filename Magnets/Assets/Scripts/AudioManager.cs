using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{   
    public Sound[] sounds;

    public static AudioManager instance;

    public AudioMixer audioMixer;

    void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        LoadSounds();
    }

    void LoadSounds()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.audioMixerGroup;
        }
    }

    void Start()
    {
        Play("GameMusic");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {   
            Debug.LogWarning("Sound:" + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void ChangeVolume(string name,float volume)
    {
        if(volume < 0 || volume > 1.0f)
        {
            Debug.LogWarning("Volume can only lie between 0.0 and 1.0f.");
            return;
        }
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {   
            Debug.LogWarning("Sound:" + name + " not found!");
            return;
        }
        s.source.volume = volume;
    }

    public void HighlightSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {   
            Debug.LogWarning("Sound:" + name + " not found!");
            return;
        }
        foreach(Sound so in sounds)
        {
            if(so.name == s.name) { so.volume = 1.0f;   continue;}
            if(so.name.Equals("GameMusic")) { so.volume = 0.06f; continue;}
            ChangeVolume(so.name,0.01f);
        }
    }

    public void ResetSoundProperties(string name)
    {
        if(name.Equals("All"))
        {
            foreach(Sound so in sounds)
            {
                so.source.clip = so.clip;
                so.source.volume = so.volume;
                so.source.pitch = so.pitch;
                so.source.loop = so.loop;
            }
            return;
        }
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {   
            Debug.LogWarning("Sound:" + name + " not found!");
            return;
        }
        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;
    }

    public void Play(string name,float volume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {   
            Debug.LogWarning("Sound:" + name + " not found!");
            return;
        }
        s.source.volume = s.volume;
        s.source.volume = s.source.volume * volume;
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {   
            Debug.LogWarning("Sound:" + name + " not found!");
            return;
        }
        s.source.Stop();
    }
}
