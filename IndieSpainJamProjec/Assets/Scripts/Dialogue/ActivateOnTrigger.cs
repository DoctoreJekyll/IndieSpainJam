using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnTrigger : MonoBehaviour
{

    //Activa un objeto cuando el player entra en un trigger y lo desactiva cuando se va, puede servir para mas cosas ademas del dialogo
    [SerializeField] private GameObject objToActivate;
    public Animator skeletonAnimator;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            objToActivate.SetActive(true);
            skeletonAnimator.Play("TALK");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            objToActivate.SetActive(false);
            skeletonAnimator.Play("IDLE");
        }
    }
}
