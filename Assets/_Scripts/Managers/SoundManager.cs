using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : PersistentSingleton<SoundManager>
{
    [SerializeField] private AudioSource _musicSource, _effectsSource;

    private float _startingMusicSourceVolume;
    private float _startingEffectsSourceVolume;

    protected override void Awake()
    {
        base.Awake();

        _startingEffectsSourceVolume = _effectsSource.volume;
        _startingMusicSourceVolume = _musicSource.volume;
    }
    public void PlaySound(AudioClip clip)
    {
        //_effectsSource.clip = clip;
        //_effectsSource.Play();
        _effectsSource.PlayOneShot(clip);
    }

    public void StopSound()
    {
        _effectsSource.Stop();
    }

    internal void TurnOffSound()
    {
        _effectsSource.volume = 0;
        _musicSource.volume = 0;
    }

    internal void TurnOnSound()
    {
        _effectsSource.volume = _startingEffectsSourceVolume;
        _musicSource.volume = _startingMusicSourceVolume;
    }
}
