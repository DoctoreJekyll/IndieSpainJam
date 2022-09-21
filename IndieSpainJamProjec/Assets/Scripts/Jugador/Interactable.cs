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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))//TODO hay que meter todo esta funcionalidad dentro del animation
        {
            DetectStuffs();
        }
    }

    
    private void DetectStuffs()//Podemos llamar también esto al atacar y generará un bool si colisiona con un enemigo
    {
        Collider2D[] hitSomething = Physics2D.OverlapCircleAll(transform.position, range, layers);

        foreach (Collider2D enemy in hitSomething)
        {
            Debug.Log("Hit that enemy" + enemy.name);
            //Creamos la interfaz/objeto, si existe llamamos a su metodo/contrato establecido que en función de lo que sea hará una cosa u otra
            //Es decir, el doactivate del enemigo no hara lo mismo que el de una palanca pero ese no es el problema del player
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
