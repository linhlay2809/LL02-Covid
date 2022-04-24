using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MainBehaviour
{
    [SerializeField] protected AudioSource audioSource;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAudioScource();
    }

    private void LoadAudioScource()
    {
        if (audioSource != null) return;
        this.audioSource = GetComponent<AudioSource>();
        Debug.Log(transform.name + " Load AudioScource");
    }

    public void ChangeMusicVolume(float value)
    {
        SoundManager.instance.musicVolume = value;
    }
    
    public void ChangeVFXVolume(float value)
    {
        SoundManager.instance.vfxVolume = value;
    }
}
