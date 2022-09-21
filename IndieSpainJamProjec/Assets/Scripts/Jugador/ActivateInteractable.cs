using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateInteractable : MonoBehaviour
{

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
        StartCoroutine(ActivateBoxObjs());
    }

    private IEnumerator ActivateBoxObjs()//Esto se a ciencia cierta de que hay una forma mejor de hacerlo pero no se me ocurre ahora, perdón.
    {
        if (_spriteRenderer.flipX == false)
        {
            activRigth.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            activRigth.SetActive(false);
            activLeft.SetActive(false);
        }
        else
        {
            activLeft.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            activLeft.SetActive(false);
            activRigth.SetActive(false);
        }
    }

}
