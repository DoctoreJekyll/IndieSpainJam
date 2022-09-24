using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
        CinemachineNoise.instance.ShakeCamera(1f,0.25f);
        //CameraShake.instance.ShakeCamera(CameraShake.ShakeMagnitude.SMALL);
        _audioSource.PlayOneShot(iceAppearSong);
    }


    public void IceImpactClip()
    {
        _audioSource.pitch = Random.Range(0.9f, 1f);
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
