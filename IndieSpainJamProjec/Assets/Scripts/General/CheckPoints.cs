using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{

    private TempManager tempManager;
    private PlayerCheckPointValues playerCheckPointValues;
    public float checkPointTemperature;
    public Vector3 playerCheckPointPos;

    private void Awake()
    {
        tempManager = FindObjectOfType<TempManager>();
    }
    
    //Cuando entro en el trigger busco el componente "playercheck.." donde guardo los datos del jugador
    //al pasar por el trigger para posteriormente cada vez que muero coger y recargar estos datos
    //Estos datos se actualizan cada vez que pasas por un check por lo que volveras al ultimo check
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerCheckPointValues playerDeath = col.GetComponentInParent<PlayerCheckPointValues>();
            playerDeath.SaveDataPlayersToCheckPoint(col);
        }
    }
}
