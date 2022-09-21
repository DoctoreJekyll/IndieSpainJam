using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //Esto es otra opción pero no me acaba de convencer.
    //TODO cosa de activar cosas

    [SerializeField] private LayerMask layers;
    [SerializeField] private float range;

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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DetectStuffs();
        }
    }


    //Aqui en el animator llamamos a este método que terminare en su momento, no se si en este script o en otro, la cosa es que
    //esto activara un detector y si da true llamamos a segun que cosas.
    private void DetectStuffs()//Podemos llamar también esto al atacar y generará un bool si colisiona con un enemigo
    {
        Collider2D[] hitSomething = Physics2D.OverlapCircleAll(transform.position, range, layers);

        foreach (Collider2D enemy in hitSomething)
        {
            Debug.Log("Hit that enemy" + enemy.name);
            //Do stuffs en este caso hacer daño al enemigo o activable segun el layer
            //Hay que darle una vuelta a esto para que puediera ser funcional tanto con enemigos como con activables
            //En cualquier caso llamariamos a las interfaces
            IActivable interactable = enemy.GetComponent<IActivable>();
            if (interactable == null)
            {
                return;
            }
            interactable.DoActivate();

        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
