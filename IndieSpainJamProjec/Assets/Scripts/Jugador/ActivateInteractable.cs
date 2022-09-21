using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateInteractable : MonoBehaviour
{

    // ESTE SCRIPT ACTUALMENTE ESTA DEPRECATED PERO LO MANTENGO POR AHORA
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private GameObject activRigth;
    [SerializeField] private GameObject activLeft;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            ActivateInteractableBox();
        }
        
    }

    private void ActivateInteractableBox()//Por ahora se activan asi, más adelante lo ideal es activarlo con las animaciones, es más cómodo y queda mejor, ya tocaremos esto
    {
        
    }


}
