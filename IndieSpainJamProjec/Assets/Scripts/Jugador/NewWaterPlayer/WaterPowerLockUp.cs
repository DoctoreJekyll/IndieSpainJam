using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPowerLockUp : MonoBehaviour, IDataPersistance
{

    [SerializeField] private GameObject waterPowerObj;
    [SerializeField] private bool waterPowerUnlock;


    private void Update()
    {
        waterPowerObj.SetActive(waterPowerUnlock);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("WaterPowerUp"))
        {
            col.gameObject.SetActive(waterPowerUnlock);
            waterPowerUnlock = true;
        }
    }
    
    public void LoadData(GameData data)
    {
        waterPowerUnlock = data.waterPowerIsUnlock;
    }

    public void SaveData(GameData data)
    {
        data.waterPowerIsUnlock = waterPowerUnlock;
    }
}
