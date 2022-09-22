using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableGameObject : MonoBehaviour
{

    [SerializeField] private LayerMask layerWhoBrokeTheObj;
    [SerializeField] private float range;
    
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
    
    // private void OnTriggerEnter2D(Collider2D col)
    // {
    //     if (col.gameObject.CompareTag("Player") && col.gameObject.layer == 7)
    //     {
    //
    //         if (col.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0)
    //         {
    //             this.gameObject.SetActive(false);
    //         }
    //         
    //     }
    // }

    private void Update()
    {
        CheckIfIcePlayerIsOn();
    
        if (CheckIfIcePlayerIsOn())
        {
            this.gameObject.SetActive(false);
        }
    }
    
    private bool CheckIfIcePlayerIsOn()
    {
        Vector2 rayDir = new Vector2(0, 0.5f);
        Vector2 rayOrigin = transform.position;
        Vector2 rayOriginLeft = new Vector2(transform.position.x - 0.6f, transform.position.y);
        Vector2 rayOriginRigth = new Vector2(transform.position.x + 0.6f, transform.position.y);
        
        RaycastHit2D hitInfo = Physics2D.Raycast(rayOrigin, rayDir, range, layerWhoBrokeTheObj);
        RaycastHit2D hitInfoLeft = Physics2D.Raycast(rayOriginLeft, rayDir, range, layerWhoBrokeTheObj);
        RaycastHit2D hitInfoRight = Physics2D.Raycast(rayOriginRigth, rayDir, range, layerWhoBrokeTheObj);
        Color rayColor = Color.green;
        if (hitInfo == true || hitInfoLeft == true || hitInfoRight == true)
        {
            return true;
        }
    
        Debug.DrawRay(transform.position, rayDir * range, rayColor);
        Debug.DrawRay(rayOriginLeft, rayDir * range, rayColor);
        Debug.DrawRay(rayOriginRigth, rayDir * range, rayColor);
        return false;
    }
    
    
}
