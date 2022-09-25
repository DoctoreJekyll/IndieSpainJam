using System;
using UnityEngine.Events;
using UnityEngine;

public class LeverEvents : MonoBehaviour, IActivable
{

    [SerializeField] private UnityEvent myEvent;

    private AudioSource _audioSource;
    public AudioClip leverClip;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void DoActivate()
    {
        //CinemachineNoise.instance.ShakeCamera(1f, 0.25f);
        CameraShake.instance.ShakeCamera(CameraShake.ShakeMagnitude.SMALL);
        myEvent.Invoke();
        _audioSource.PlayOneShot(leverClip);
    }
}
