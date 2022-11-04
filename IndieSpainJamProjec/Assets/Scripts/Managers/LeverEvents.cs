using System;
using UnityEngine.Events;
using UnityEngine;

public class LeverEvents : MonoBehaviour, IActivable
{

    [SerializeField] private UnityEvent myEvent;

    private AudioSource _audioSource;
    public AudioClip leverClip;

    private bool isNotActivate;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        isNotActivate = true;
    }

    public void DoActivate()
    {
        
        if (isNotActivate)
        {
            _audioSource.PlayOneShot(leverClip);
            //CameraShake.instance.ShakeCamera(CameraShake.ShakeMagnitude.SMALL);
            CinemachineNoise.instance.ShakeCamera(1f, 0.25f);
            myEvent.Invoke();
            isNotActivate = false;
        }
 
    }
}
