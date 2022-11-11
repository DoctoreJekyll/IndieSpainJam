using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckPointValues : MonoBehaviour
{

    private TempManager tempManager;
    
    public Vector3 playerCheckPoinPositionValue;
    public float checkPointTemperatureValue;


    private void Awake()
    {
        tempManager = FindObjectOfType<TempManager>();
    }

    //Metodo exclusivamente para recoger valores generales ya que estamos constantemente intercambiando
    //entre jugadores, necesito tirar de aquí para recoger valores globales.
    public void SaveDataPlayersToCheckPoint(Collider2D collider2D)
    {
        playerCheckPoinPositionValue = collider2D.gameObject.transform.position;
        checkPointTemperatureValue = tempManager.currentTemp;
    }


}
