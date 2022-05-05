using UnityEngine.Audio;
using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public float musicVolume = 1f;
    public float vfxVolume = 1f;

    public Sound[] sounds;
    
    private static SoundManager instance;
    public static SoundManager Instance => instance;
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            if(s.isMusic) s.source.loop = true;
        }
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.Play();
    }

    public void LoadVolume()
    {
        foreach (Sound s in sounds)
        {
            if (s.isMusic) 
                s.source.volume = s.volume * this.musicVolume;
            else 
                s.source.volume = s.volume * this.vfxVolume;
        }
    }
}
