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
    ClickDisabled,
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

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void Play(SoundType soundType)
    {
        var soundData = this.soundData.Find(x => x.Type == soundType);
        if (soundData == null || soundData.AudioClip == null)
        {
            return;
        }
        PlaySound(soundData);
    }

    private void PlaySound(SoundData sound)
    {
        soundSource.volume = sound.Volume;
        soundSource.pitch = sound.Pitch;
        soundSource.PlayOneShot(sound.AudioClip);
    }
}
