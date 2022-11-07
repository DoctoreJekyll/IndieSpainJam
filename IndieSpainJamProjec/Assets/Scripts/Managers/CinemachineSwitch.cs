using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class CinemachineSwitch : MonoBehaviour
{
    [SerializeField] private InputAction action;
    private Animator animator;
    private Vector2 directionValue;
    private float value;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
        action.Enable();
        action.performed += ctx => directionValue = ctx.ReadValue<Vector2>();
        action.canceled += ctx => directionValue = Vector2.zero;
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        action.Disable();
    }

    private void Update()
    {
        value = directionValue.y;
        
        Debug.Log(value);
        
        SwitchesBetweenCameras();
    }

    private void SwitchesBetweenCameras()
    {
        if (value > 0.9f && directionValue.x == 0)
        {
            animator.Play("TOPCamera");
            Debug.Log("Camara top");
        }
        else if (value < -0.9f && directionValue.x == 0)
        {
            animator.Play("BOTCamera");
            Debug.Log("Camara bot");
        }
        else
        {
            animator.Play("MainCamera");
            Debug.Log("Pon la camra normal dfsf");
        }
    }
    
}
