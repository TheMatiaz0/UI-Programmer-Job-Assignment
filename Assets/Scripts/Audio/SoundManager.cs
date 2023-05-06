using System;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    None,
    ButtonClick,
    ButtonClose,
    ButtonSelect,
    ButtonSelectAlt,
    ToggleSelect,
    ToggleClick,
    SelectDisabled,
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
        private float volume = 1;
        [SerializeField]
        private float pitch = 1;

        public SoundType Type => type;
        public AudioClip AudioClip => audioClip;
        public float Volume => volume;
        public float Pitch => pitch;
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
        soundSource.pitch = sound.Pitch;
        soundSource.clip = sound.AudioClip;
        soundSource.Play();
    }
}
