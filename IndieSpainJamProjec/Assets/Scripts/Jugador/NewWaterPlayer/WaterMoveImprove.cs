using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaterMoveImprove : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private Vector2 direction;
    private HydroMorpher playerInputsActions;
    private float moveInput;
    private Animator waterAnimator;
    private PlayerJump playerJump;
    private bool isFacinRigth;
    
    [Header("Configuration Values")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float runAcceleration; //Time (approx.) time we want it to take for the player to accelerate from 0 to the runMaxSpeed.
    [SerializeField] private float runDecceleration; //Time (approx.) we want it to take for the player to accelerate from runMaxSpeed to 0.
    [SerializeField] private float velPower;
    [SerializeField] private float lerpAmount;
    [SerializeField] private float moveSpeedWhenSpikes;
    [SerializeField] private float moveSpeedOnAir;
    [SerializeField] private float fricctionAmount;
    private float maxMoveSpeed;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        waterAnimator = GetComponent<Animator>();
        playerJump = GetComponent<PlayerJump>();
        SetNewPlayerInput();
    }

    private void Start()
    {
        maxMoveSpeed = moveSpeed;
    }

    private void SetNewPlayerInput()
    {
        playerInputsActions = new HydroMorpher();
        playerInputsActions.Enable();

        //TODO Si esto soluciona el bug hay que cambiarlo en los demas jugadores
        playerInputsActions.PlayerInputs.Move.performed += ctx => direction = ctx.ReadValue<Vector2>();
        playerInputsActions.PlayerInputs.Move.canceled += ctx => direction = Vector2.zero;

        //direction = playerInputsActions.PlayerInputs.Move.ReadValue<Vector2>().normalized;
    }
    
    //Segunda solucion
    public void MoveSolution(InputAction.CallbackContext context)
    {
        //Comprobar si esto se soluciona como un float
        direction = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        moveInput = direction.x;
        
        if (GameStateManager.instance.currentGameState == GameStateManager.GameState.GAMEPLAY)
        {
            AnimationMovement(moveInput);
            MoveOnJump();
        } 
    }

    private void FixedUpdate()
    {
        if (GameStateManager.instance.currentGameState == GameStateManager.GameState.GAMEPLAY)
        {
            PlayerCanRun();
            Flip();
        }

    }

    public bool momentum;

    private void Run()
    {
        //Calcula la direccion y la velocidad
        float targetSpeed = moveInput * moveSpeed;
        targetSpeed = Mathf.Lerp(rb2d.velocity.x, targetSpeed, lerpAmount);
        //Diferencia entre vel actual y la deseada
        float sppedDif = targetSpeed - rb2d.velocity.x;
        //Cambia el ratio de acelerar segun estamos acelerando o decelerando(creo que esta segunda palabra me la he inventado)
        float accelRate;

        #region Momentun Test
        //Test Momentun
        if (momentum)
        {
            if (Mathf.Abs(rb2d.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(rb2d.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && !playerJump.isOnFloor)
            {
                accelRate = 0;
            }
            else
            {
                accelRate = (Math.Abs(targetSpeed) > 0.01f) ? runAcceleration : runDecceleration;
            }
        }
        else
        {
            accelRate = (Math.Abs(targetSpeed) > 0.01f) ? runAcceleration : runDecceleration;
        }
        //end
        #endregion
        
        //Aplica aceleracion a la speedDif en resumen
        //float movement = (float)(Math.Pow(Math.Abs(sppedDif) * accelRate, velPower) * Math.Sign(sppedDif));
        float movement = sppedDif * accelRate;

        rb2d.AddForce(movement * Vector2.right);
    }
    
    private void MoveOnJump()
    {
        if (!playerJump.isOnFloor)
        {
            moveSpeed = moveSpeedOnAir;
        }
        else
        {
            moveSpeed = maxMoveSpeed;
        }
    }

    private void Friction()
    {
        if (playerJump.isOnFloor && Mathf.Abs(moveInput) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(rb2d.velocity.x), Mathf.Abs(fricctionAmount));
            amount *= Mathf.Sign(rb2d.velocity.x);
            rb2d.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }
    
    private void PlayerCanRun()
    {
        if (GameStateManager.instance.currentGameState == GameStateManager.GameState.GAMEPLAY)
        {
            Run();
            Friction();
        }    
        else
        {
            rb2d.velocity = new Vector2(0f, rb2d.velocity.y);//Ñapa temporal
        }
    }
    
    private void Flip()
    {
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);

        }
        
        if (direction.x < 0)
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
    
    private void AnimationMovement(float movement)
    {
        if(playerJump.isOnFloor)
        {
            if(movement != 0)
            {
                waterAnimator.SetBool("RUNNING", true);
                waterAnimator.SetBool("IDLE", false);
            }
            else
            {
                waterAnimator.SetBool("RUNNING", false);
                waterAnimator.SetBool("IDLE", true);
            }
        }
        else
        {
            waterAnimator.SetBool("RUNNING", false);
            waterAnimator.SetBool("IDLE", false);
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 3)
        {
            moveSpeed = moveSpeedWhenSpikes;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 3)
        {
            moveSpeed = maxMoveSpeed;
        }
    }

}
