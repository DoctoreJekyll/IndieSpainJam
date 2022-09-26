using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GasPlayerSound : MonoBehaviour
{
    
    [SerializeField] private AudioSource _audioSource;
    
    [Header("Sounds")] 
    [SerializeField] private AudioClip gasAppearSong;
    [SerializeField] private AudioClip littleMovesClip;
    
    [Header("Effects")]
    [SerializeField] private GameObject particle;
    
    private void OnEnable()
    {
        _audioSource.PlayOneShot(gasAppearSong);
        //CinemachineNoise.instance.ShakeCamera(1f,0.25f);
        particle.SetActive(true);
        CameraShake.instance.ShakeCamera(CameraShake.ShakeMagnitude.MEDIUM);
    }


    public void LittleMoves()
    {
        //_audioSource.loop = enabled;
        if (!_audioSource.isPlaying)
        {
            _audioSource.pitch = Random.Range(0.9f, 1f);
            _audioSource.loop = enabled;
            _audioSource.PlayOneShot(littleMovesClip);
        }

    }

    public void StopMovement()
    {
        _audioSource.loop = false;
        _audioSource.Stop();

    }
    

}
