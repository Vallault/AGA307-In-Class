using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioClip[] enemyHitSounds;
    public AudioClip[] enemyDieSounds;
    public AudioClip[] enemyAttackSounds;
    public AudioClip[] Footsteps; 

    public AudioClip GetEnemyHitSound()
    {
        return enemyHitSounds[Random.Range(0, enemyHitSounds.Length)];
    }

    public AudioClip GetEnemyDieSound()
    {
        return enemyDieSounds[Random.Range(0, enemyDieSounds.Length)];
    }

    public AudioClip GetEnemyAttackSound()
    {
        return enemyAttackSounds[Random.Range(0, enemyAttackSounds.Length)];
    }

    public AudioClip FootstepsSounds()
    {
        return Footsteps[Random.Range(0, Footsteps.Length)];
    }
    /// <summary>
    /// Plays an Audio Clip with adjusted pitch values
    /// </summary>
    /// <param name="_clip">The clip to play</param>
    /// <param name="_source">The audio source to play om</param>
    public void PlaySound(AudioClip _clip, AudioSource _source, float _volume = 1)
    {
        if (_source == null || _clip == null)
            return;

        _source.clip = _clip;
        _source.pitch = Random.Range(0.8f, 1.2f);
        _source.volume = _volume;
        _source.Play();
    }
}
