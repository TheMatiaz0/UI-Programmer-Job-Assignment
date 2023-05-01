using System;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    None,
    ButtonNormal,
    ButtonClose,
    ButtonPick,
    ButtonPickAlt,
}

public class SoundManager : MonoBehaviour
{
    [Serializable]
    private class SoundData
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
        if (sound == null || sound.AudioClip == null)
        {
            return;
        }
        playlist.Enqueue(sound);
    }

    private void PlaySound(SoundData sound)
    {
        soundSource.volume = sound.Volume;
        soundSource.clip = sound.AudioClip;
        soundSource.Play();
    }
}
