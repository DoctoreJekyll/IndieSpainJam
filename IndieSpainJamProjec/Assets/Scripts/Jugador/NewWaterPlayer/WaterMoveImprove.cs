using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMoveImprove : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private Vector2 direction;
    private HydroMorpher playerInputsActions;
    private float moveInput;
    private Animator waterAnimator;
    
    [Header("Configuration Values")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float runAcceleration; //Time (approx.) time we want it to take for the player to accelerate from 0 to the runMaxSpeed.
    [SerializeField] private float runDecceleration; //Time (approx.) we want it to take for the player to accelerate from runMaxSpeed to 0.
    [SerializeField] private float velPower;
    [SerializeField] private float lerpAmount;
    [SerializeField] public float moveSpeedWhenSpikes;
    private float maxMoveSpeed;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        waterAnimator = GetComponent<Animator>();
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
    }

    private void Update()
    {
        direction = playerInputsActions.PlayerInputs.Move.ReadValue<Vector2>();
        moveInput = direction.x;
        
        if (GameStateManager.instance.currentGameState == GameStateManager.GameState.GAMEPLAY)
        {
            FlipAndAnimation();
        } 
    }

    private void FixedUpdate()
    {
        PlayerCanRun();
    }

    private void Run()
    {
        //Calcula la direccion y la velocidad
        float targetSpeed = moveInput * moveSpeed;
        targetSpeed = Mathf.Lerp(rb2d.velocity.x, targetSpeed, lerpAmount);
        //Diferencia entre vel actual y la deseada
        float sppedDif = targetSpeed - rb2d.velocity.x;
        //Cambia el ratio de acelerar segun estamos acelerando o decelerando(creo que esta segunda palabra me la he inventado)
        float accelRate = (Math.Abs(targetSpeed) > 0.01f) ? runAcceleration : runDecceleration;
        //Aplica aceleracion a la speedDif en resumen
        float movement = (float)(Math.Pow(Math.Abs(sppedDif) * accelRate, velPower) * Math.Sign(sppedDif));
        //float movement = sppedDif * accelRate;
	    
        rb2d.AddForce(movement * Vector2.right);
    }
    
    private void PlayerCanRun()
    {
        if (GameStateManager.instance.currentGameState == GameStateManager.GameState.GAMEPLAY)
        {
            Run();
        }    
        else
        {
            rb2d.velocity = new Vector2(0f, rb2d.velocity.y);//Ñapa temporal
        }
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
