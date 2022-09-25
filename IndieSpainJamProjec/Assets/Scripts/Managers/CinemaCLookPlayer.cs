using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemaCLookPlayer : MonoBehaviour
{
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private Transform playerTransform;

    private void Awake()
    {
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    

    // Update is called once per frame
    void Update()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        _cinemachineVirtualCamera.Follow = playerTransform;
    }
}
