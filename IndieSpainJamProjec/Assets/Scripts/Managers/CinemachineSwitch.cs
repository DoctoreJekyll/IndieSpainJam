using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class CinemachineSwitch : MonoBehaviour
{
    [SerializeField] private InputAction action;
    private Animator animator;
    private Vector2 directionValue;
    private float value;

    public bool canLookAround;//TODO Este bool podemos usarlo para generar zonas donde no puedas hacer el look around

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
        action.Enable();
        action.performed += ctx => directionValue = ctx.ReadValue<Vector2>();
        action.canceled += ctx => directionValue = Vector2.zero;
    }

    private void OnDisable()
    {
        action.Disable();
    }

    private void Update()
    {
        value = directionValue.y;
        Debug.Log(directionValue.x);
        Debug.Log(directionValue.y);
        SwitchesBetweenCameras();
    }

    //TODO Ahora mismo a pesar de que la X no sea 0, reproduce la animacion de cambio de cámara
    //La X vale 0 cuando no deberia, probar a generar los inputs en el input system y no en un action dentro de un script
    private void SwitchesBetweenCameras()
    {
        if (value > 0.9f && directionValue.x == 0)
        {
            animator.Play("TOPCamera");
        }
        else if (value < -0.9f && directionValue.x == 0)
        {
            animator.Play("BOTCamera");
        }
        else
        {
            animator.Play("MainCamera");
        }
    }
    
}
