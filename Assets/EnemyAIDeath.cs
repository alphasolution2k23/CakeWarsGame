using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyAIDeath : MonoBehaviour
{
    public EnemyType enemyType;
    public AudioMixerGroup mixer;

    private void Start()
    {
        Invoke(nameof(SetAudioMixer), 0.2f);
    }

    private void SetAudioMixer()
    {
        AudioSource[] audios = GetComponents<AudioSource>();

        foreach (AudioSource audio in audios)
        {
            audio.outputAudioMixerGroup = mixer;
        }
    }
}
