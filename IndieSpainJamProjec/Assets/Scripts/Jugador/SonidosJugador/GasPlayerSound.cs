using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasPlayerSound : MonoBehaviour
{
    
    [SerializeField] private AudioSource _audioSource;
    
    [Header("Sounds")] 
    [SerializeField] private AudioClip gasAppearSong;
    [SerializeField] private AudioClip littleMovesClip;
    

    private void OnEnable()
    {
        _audioSource.PlayOneShot(gasAppearSong);
    }

    public void LittleMoves()
    {
        _audioSource.PlayOneShot(littleMovesClip);
    }

}
