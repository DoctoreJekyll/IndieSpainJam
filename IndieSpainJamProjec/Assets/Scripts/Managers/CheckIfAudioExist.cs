using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfAudioExist : MonoBehaviour
{

    [SerializeField] private GameObject audioGO;

    private void Start()
    {
        if (audioGO.scene.IsValid())
        {
            return;
        }
        else
        {
            Instantiate(audioGO);
        }
    }
}
