using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum SoundType
{
    ButtonNormal,
    Maximize,
    Minimize,
    ButtonAlternative,
}

[Serializable]
public class SoundData
{
    [SerializeField]
    private SoundType type;
    [SerializeField]
    private AudioClip audioClip;
    [SerializeField]
    [Range(0f, 1f)]
    private float volume;

    public SoundType Type => type;
    public AudioClip AudioClip => audioClip;
    public float Volume => volume;
}

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private List<SoundData> soundData;
    [SerializeField]
    private AudioSource soundSource;

    private readonly Queue<SoundData> playlist = new();

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (playlist.Count > 0 && !soundSource.isPlaying)
        {
            var sound = playlist.Dequeue();
            PlaySound(sound);
        }
    }

    public void Play(SoundType type)
    {
        var sound = soundData.Find(x => x.Type == type);
        playlist.Enqueue(sound);
    }

    private void PlaySound(SoundData sound)
    {
        soundSource.volume = sound.Volume;
        soundSource.clip = sound.AudioClip;
        soundSource.Play();
    }
}
