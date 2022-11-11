using UnityEngine;
using UnityEngine.InputSystem;
public class CinemachineSwitch : MonoBehaviour
{
    private HydroMorpher playerInputsActions;
    
    private Animator animator;
    private Vector2 directionValue;
    private float value;
    private Rigidbody2D playerRb2D;

    public bool canLookAround;//TODO Este bool podemos usarlo para generar zonas donde no puedas hacer el look around
    public bool playerIsInGas;//TODO alomejor no queremos que el player modo gas pueda mirar arriba o abajo

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
        SetNewPlayerInput();
    }

    private void SetNewPlayerInput()
    {
        playerInputsActions = new HydroMorpher();
        playerInputsActions.Enable();
    }

    private void OnDisable()
    {
        playerInputsActions.Disable();
    }

    private void Update()
    {
        directionValue = playerInputsActions.PlayerInputs.Move.ReadValue<Vector2>();
        SwitchesBetweenCameras();
    }
    
    //Segun los valores recogidos por inputs la cámara actua de un modo u otro llamando a un animator
    private void SwitchesBetweenCameras()
    {
        if (directionValue.y > 0.9f && directionValue.x == 0)
        {
            animator.SetBool("canTop", true);
        } 
        else
        {
            animator.SetBool("canTop", false);
        }
        
        if (directionValue.y < -0.9f && directionValue.x == 0)
        {
            animator.SetBool("canBot", true);
        }
        else
        {
            animator.SetBool("canBot", false);
        }
    }
    
}
