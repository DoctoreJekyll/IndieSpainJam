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

    [Header("Effects")] 
    [SerializeField] private GameObject particle;

    /// <summary>
    /// ///Control de daños
    /// </summary>
    public PlayerDeath playerDeath;
    private bool checkDead;
    
    private void OnEnable()
    {
        _audioSource.GetComponent<AudioSource>();
        
        //CameraShake.instance.ShakeCamera(CameraShake.ShakeMagnitude.MEDIUM);//Esto da fallos la primera vez porque este enable entra antes que la instancia de la camara, hay que hacer apaños
        _audioSource.PlayOneShot(waterAppearSong);
        particle.SetActive(true);
        checkDead = false;
    }

    private void Update()
    {
        
        if (playerDeath.dead == true && checkDead == false)
        {
            DeadSound();
            checkDead = true;
        }
    }

    public void Step()//Este metodo se está usando en el animator
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
        _audioSource.pitch = Random.Range(0.85f, 1f);
        _audioSource.PlayOneShot(jumpClip);
    }

    public void FallSound()
    {
        _audioSource.pitch = Random.Range(0.85f, 1f);
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
