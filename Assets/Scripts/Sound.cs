using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public bool isSFX;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volumn = .7f;
    [Range(.5f, 1.5f)]
    public float pitch = 1f;

    [Range(0f, .5f)]
    public float randomVolumn = .1f;
    [Range(0f, .5f)]
    public float randomPitch = .1f;

    public bool isLooped;
    [HideInInspector]
    public AudioSource source;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play()
    {
        source.volume = volumn * (1 + Random.Range(-randomVolumn / 2f, randomVolumn / 2f));
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
        source.loop = isLooped;
        source.Play();
    }
}