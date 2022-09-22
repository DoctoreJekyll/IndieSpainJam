using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableGameObject : MonoBehaviour
{
    private AirController _airController;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (col.GetComponent<AirController>() == null)
            {   
                return;
            }

            _airController = col.GetComponent<AirController>();
            if (_airController.IsOnAir())
            {
                gameObject.SetActive(false);
            }

        }
    }
}
