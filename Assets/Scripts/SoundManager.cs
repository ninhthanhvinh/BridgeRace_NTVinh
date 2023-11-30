using System;
using System.Collections;
using System.Collections.Generic;
using Tayx.Graphy.Audio;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField]
    Sound[] sounds;

    public AudioMixerGroup musicMixer;
    public AudioMixerGroup sfxMixer;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            //Instance = this;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
            if (sounds[i].isSFX)
            {
                sounds[i].source.outputAudioMixerGroup = sfxMixer;
            }
            else
            {
                sounds[i].source.outputAudioMixerGroup = musicMixer;
            }
        }
    }

    void Start()
    {

    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }

        // no sound with _name
        Debug.LogWarning("Audio Manager: Sound not found");
    }
}