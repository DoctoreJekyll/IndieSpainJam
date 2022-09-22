using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonSmashed : MonoBehaviour
{

    [Header("Button Configure")]
    [SerializeField] private float raycastRange;
    [SerializeField] private LayerMask layerWhoActivate;
    [SerializeField] private bool buttonIsUsed;
    
    [Header("Events to start")]
    [SerializeField] private UnityEvent buttonEvent;

    private void Start()
    {
        buttonIsUsed = false;
    }

    private void Update()
    {
        if (!buttonIsUsed && CheckIfIcePlayerIsOn())
        {
            Debug.Log("Call the action");
            buttonEvent.Invoke();
            buttonIsUsed = true;
        }
    }

    private bool CheckIfIcePlayerIsOn()
    {
        Vector2 rayDir = new Vector2(0, 0.5f);
        Vector2 rayOriginLeft = new Vector2(transform.position.x - 0.2f, transform.position.y);
        Vector2 rayOriginRigth = new Vector2(transform.position.x + 0.2f, transform.position.y);
        
        
        RaycastHit2D hitInfoLeft = Physics2D.Raycast(rayOriginLeft, rayDir, raycastRange, layerWhoActivate);
        RaycastHit2D hitInfoRight = Physics2D.Raycast(rayOriginRigth, rayDir, raycastRange, layerWhoActivate);
        Color rayColor = Color.green;
        if (hitInfoLeft == true || hitInfoRight == true)
        {
            return true;
        }
    
        Debug.DrawRay(transform.position, rayDir * raycastRange, rayColor);
        Debug.DrawRay(rayOriginLeft, rayDir * raycastRange, rayColor);
        Debug.DrawRay(rayOriginRigth, rayDir * raycastRange, rayColor);
        return false;
    }
}
