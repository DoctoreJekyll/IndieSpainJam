using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlayerSounds : MonoBehaviour
{

    [SerializeField] private AudioSource _audioSource;
    
    [Header("Sounds")] 
    [SerializeField] private AudioClip iceAppearSong;
    [SerializeField] private AudioClip iceImpactClip;
    [SerializeField] private AudioClip breakGroundClip;
    [SerializeField] private AudioClip deadClip;

    private void OnEnable()
    {
        CameraShake.instance.ShakeCamera(CameraShake.ShakeMagnitude.SMALL);
        _audioSource.PlayOneShot(iceAppearSong);
    }


    public void IceImpactClip()
    {
        _audioSource.PlayOneShot(iceImpactClip);
    }

    public void BreakGround()
    {
        _audioSource.PlayOneShot(breakGroundClip);
    }

    public void DeadClip()
    {
        _audioSource.PlayOneShot(deadClip);
    }

}
