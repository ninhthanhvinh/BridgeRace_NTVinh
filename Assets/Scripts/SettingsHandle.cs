using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsHandle : MonoBehaviour
{
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;
    public void MusicVolumnChange(float value)
    {
        musicMixer.SetFloat("MusicVolumn", Mathf.Log10(value) * 20 + 20);
    }

    public void SFXVolumnChange(float value)
    {
        sfxMixer.SetFloat("SFXVolumn", Mathf.Log10(value) * 20 + 20);
    }
}