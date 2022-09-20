using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //Esto es otra opción pero no me acaba de convencer.
    //TODO cosa de activar cosas
    private void OnTriggerEnter2D(Collider2D col)
    {
        
        Debug.Log("Is Triggered");
        if (col.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy is damaged!");
        }

        if (col.gameObject.CompareTag("Activable"))
        {
            Debug.Log("Activable is activable");
        }
        
        //Activate obj to need activate
        //Dodamage if is necesary
    }

    
    //Aqui en el animator llamamos a este método que terminare en su momento, no se si en este script o en otro, la cosa es que
    //esto activara un detector y si da true llamamos a segun que cosas.
    private void DetectStuffs()
    {
        //Collider2D[] hitSomething = Physics2D.OverlapCircleAll()
    }
    
}
