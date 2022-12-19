using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugObj : MonoBehaviour
{
    private TempManager tempManager;
    
    private void Awake()
    {
        tempManager = GameObject.FindObjectOfType<TempManager>();
    }
    

    // Update is called once per frame
    void Update()
    {
        ModifyTemperatureDebug();
    }

    private void ModifyTemperatureDebug()
    {
        if (Input.GetKey(KeyCode.O))
        {
            tempManager.ModifyTemperature(5f,4f);
        }

        if (Input.GetKey(KeyCode.P))
        {
            Debug.Log("test debug");
            tempManager.ModifyTemperature(-5f,4f);
        }
    }
}
