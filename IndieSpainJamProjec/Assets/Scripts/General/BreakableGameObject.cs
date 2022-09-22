using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableGameObject : MonoBehaviour
{

    [SerializeField] private LayerMask layerWhoBrokeTheObj;
    
    //TE RECORDAREMOS CON ODIO E IRA
    // private void OnTriggerEnter2D(Collider2D col)
    // {
    //     if (col.gameObject.CompareTag("Player"))
    //     {
    //         Debug.Log("player noeke");
    //         if (col.GetComponent<AirController>())
    //         {
    //             Debug.Log("encuentra el air controller");
    //         }
    //         airController = col.GetComponent<AirController>();
    //         if (!airController.IsOnAir())
    //         {
    //             Debug.Log("test aire");
    //             this.gameObject.SetActive(false);
    //         }
    //     }
    // }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && col.gameObject.layer == layerWhoBrokeTheObj)
        {

            if (col.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0)
            {
                this.gameObject.SetActive(false);
            }
            
        }
    }
    
}
