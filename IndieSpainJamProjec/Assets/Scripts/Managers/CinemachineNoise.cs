using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineNoise : MonoBehaviour
{

    private CinemachineVirtualCamera _cinemachineVirtualCamera;

    public static CinemachineNoise instance;
    public enum ShakeMagnitude { NULL, SMALL, MEDIUM, BIG, HUGE }

    private void Awake()
    {

        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        CreateSingleton();
    }
    
    private void CreateSingleton()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ShakeCamera(2,1);
        }
    }

    public void ShakeCamera(float intensity, float time)
    {
        StartCoroutine(ShakeCoroutine(intensity, time));
    }

    private IEnumerator ShakeCoroutine(float intensity, float time)
    {
        float currenTime = 0;
        CinemachineBasicMultiChannelPerlin channelPerlin =
            _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
        while (currenTime < time)
        {
            channelPerlin.m_AmplitudeGain = intensity;
            currenTime += Time.unscaledTime;
            yield return null;
        }

        channelPerlin.m_AmplitudeGain = 0f;

    }
    
}
