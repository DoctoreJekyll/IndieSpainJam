using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaterPlayerSounds : MonoBehaviour
{

    [SerializeField] private AudioSource _audioSource;
    
    [Header("Sounds")] 
    [SerializeField] private AudioClip waterAppearSong;
    [SerializeField] private AudioClip[] stepClip;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip fallClip;
    [SerializeField] private AudioClip attackClip;
    [SerializeField] private AudioClip deadClip;

    private void OnEnable()
    {
        //CameraShake.instance.ShakeCamera(CameraShake.ShakeMagnitude.SMALL);
        _audioSource.PlayOneShot(waterAppearSong);
    }

    public void Step()
    {
        AudioClip clip = GetRandomClip();
        _audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomClip()
    {
        int index = Random.Range(0, stepClip.Length - 1);
        return stepClip[index];
    }

    public void JumpSound()
    {
        _audioSource.PlayOneShot(jumpClip);
    }

    public void FallSound()
    {
        _audioSource.PlayOneShot(fallClip);
    }

    public void AttackSound()
    {
        _audioSource.PlayOneShot(attackClip);
    }

    public void DeadSound()
    {
        _audioSource.PlayOneShot(deadClip);
        //Esto pueden ser varios o varias por la arena
    }

}
