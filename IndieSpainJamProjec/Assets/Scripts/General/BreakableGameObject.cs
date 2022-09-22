using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableGameObject : MonoBehaviour
{

    public AirController airController;
    

    private void Update()
    {


    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("player noeke");
            if (col.GetComponent<AirController>())
            {
                Debug.Log("encuentra el air controller");
            }
            airController = col.GetComponent<AirController>();
            if (airController.IsOnAir())
            {
                Debug.Log("test aire");
                this.gameObject.SetActive(false);
            }
        }
    }
}
