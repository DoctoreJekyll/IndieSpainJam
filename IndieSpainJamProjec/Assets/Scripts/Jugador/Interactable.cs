using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    
    [SerializeField] private LayerMask interactableLayers;
    [SerializeField] private float range;
    public PlayerJump playerJump;
    public Animator waterAnimator;
    public KeyCode attackKey;

    private void Update()
    {
        if (Input.GetKeyDown(attackKey) && playerJump.isOnFloor == true)//TODO hay que meter todo esta funcionalidad dentro del animation
        {
            waterAnimator.Play("IDLE");
            waterAnimator.Play("INTERACTING");

            DetectStuffs();
        }
    }

    
    private void DetectStuffs()//Podemos llamar también esto al atacar y generará un bool si colisiona con un enemigo
    {
        Collider2D[] hitSomething = Physics2D.OverlapCircleAll(transform.position, range, interactableLayers);

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
