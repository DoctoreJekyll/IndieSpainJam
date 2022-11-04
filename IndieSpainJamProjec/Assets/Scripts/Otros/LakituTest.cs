using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakituTest : MonoBehaviour
{
    private HydroMorpher playerInputsActions;
    private bool isFacinRigth;
    private Vector2 inputMoveVector;
    
    // Start is called before the first frame update
    void Start()
    {
        SetNewPlayerInput();
    }
    
    private void SetNewPlayerInput()
    {
        playerInputsActions = new HydroMorpher();
        playerInputsActions.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Transform playerTransform = GameObject.FindWithTag("Player").transform;

        if (playerTransform != null)
        {
            transform.position = playerTransform.position;
        }

        inputMoveVector = playerInputsActions.PlayerInputs.Move.ReadValue<Vector2>().normalized;
        
    }

    private void FixedUpdate()
    {
        if (inputMoveVector.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (inputMoveVector.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }


    private void FlipFunction()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        
    }
    
}
