using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfAudioExist : MonoBehaviour
{

    [SerializeField] private GameObject audioGO;

    //Si el objeto del audio no está, instanciamos el objeto para el audio
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
