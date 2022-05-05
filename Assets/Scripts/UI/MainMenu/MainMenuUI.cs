using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MainBehaviour
{
    public OptionUI optionUI;

    [SerializeField] protected AudioSource audioSource;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAudioScource();
        this.LoadOptionUI();
    }

    private void LoadOptionUI()
    {
        if (optionUI != null) return;
        this.optionUI = transform.Find("OptionUI").GetComponent<OptionUI>();
        Debug.Log(transform.name + " Load OptionUI");
    }

    private void LoadAudioScource()
    {
        if (audioSource != null) return;
        this.audioSource = GetComponent<AudioSource>();
        Debug.Log(transform.name + " Load AudioScource");
    }

    public void ChangeMusicVolume(float value)
    {
        SoundManager.Instance.musicVolume = value;
    }
    
    public void ChangeVFXVolume(float value)
    {
        SoundManager.Instance.vfxVolume = value;
    }
    public void MenuClickSound()
    {
        SoundManager.Instance.Play("MenuClick");
    }
    public void ToggleTutorial(bool value)
    {
        GameManager.Instance.SetTutorial(value);
    }
}
