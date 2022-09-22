using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableGameObject : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("player noeke");
            AirController airControllerPlayerIce = col.GetComponent<AirController>();
            if (airControllerPlayerIce.IsOnAir())
            {
                Debug.Log("test aire");
                this.gameObject.SetActive(false);
            }
        }
    }
}
