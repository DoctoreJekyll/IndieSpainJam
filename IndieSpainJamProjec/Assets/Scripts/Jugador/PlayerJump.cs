using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private WaterPlayerSounds _waterPlayerSounds;
    private Animator waterAnimator;

    [Header("Jump Stuffs")]
    [SerializeField] private float jumpForce;
    [SerializeField] private GameObject pointToCheckFloor;
    [SerializeField] private Vector2 boxCheckSize;
    [SerializeField] private LayerMask floorLayer;
    [SerializeField] private GameObject shadow;
    private bool isJumping;
    public bool isOnFloor;

    [Header("Fall Suffs")]
    public bool isOnAir;
    public Vector2 fallCheck;

    [Header("Coyote Bro")]
    [SerializeField]private float coyoteTime;
    [SerializeField]private float timeToDoCoyote;

    [Header("Particles")]
    [SerializeField] private ParticleSystem fallParticle;

    [Header("Improve Jump")]
    [SerializeField] private float jumpDelay;
    private float jumpTimer;

    private void Start()
    {
        waterAnimator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        _waterPlayerSounds = GetComponent<WaterPlayerSounds>();
    }

    private void Update()
    {
        IsOnFloor();
        FallCheck();
        CoyoteTimeImprove();
        JumpControllerAnim();

        shadow.SetActive(isOnFloor);
    }

    private void IsOnFloor()
    {
        isOnFloor = Physics2D.OverlapBox(pointToCheckFloor.transform.position, boxCheckSize, 0, floorLayer);
    }

    public void JumpAction(InputAction.CallbackContext context)//Llamamos a este metodo dentro del componente input action del playermanager 
    {
        if (GameStateManager.instance.currentGameState == GameStateManager.GameState.GAMEPLAY)
        {
            if (context.performed)
            {
                Debug.Log("Estoy pulsando el salto?");
                if (coyoteTime > 0f)
                {
                    jumpTimer = Time.time + jumpDelay;
                }
            }
            
            if (context.canceled)
            {
                Debug.Log("Suelto boton");
                coyoteTime = 0f;
            }
            
        }
    }

    private void FixedUpdate()
    {
        if (jumpTimer > Time.time && isOnFloor)
        {
            JumpMethod();
        }
    }

    private void JumpMethod()//En el primer if detecto si no está en el suelo para saltar pero por el coyote time, aunque no ests en el suelo tienes una ventana para saltar
    {
        if (!isOnFloor && !isJumping)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x,0f);
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
            jumpTimer = 0f;
        }
        else
        {
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
            jumpTimer = 0f;
        }

    }

    private void CoyoteTimeImprove()//Control del tiempo para generar el efecto coyote
    {
        if (isOnFloor)
        {
            coyoteTime = timeToDoCoyote;
        }
        else
        {
            coyoteTime -= Time.deltaTime;
        }
        
    }

    private void FallCheck()//Método para hacer el check al tocar el suelo
    {
        if (!isOnFloor)
        {
            isOnAir = true;
        }

        if (isOnAir)
        {
            bool fallCheck = Physics2D.OverlapBox(pointToCheckFloor.transform.position, this.fallCheck, 0, floorLayer);
            if (fallCheck)
            {
                _waterPlayerSounds.FallSound();
                fallParticle.Play();
                isOnAir = false;
                isJumping = false;
                //betterJump.enabled = true;
            }

        }
    }

    private void JumpControllerAnim()//Método que se encarga de controlar las anim
    {
        if(isOnFloor == false)
        {
            if(rb2d.velocity.y  > 0)
            {
                waterAnimator.SetBool("JUMPING", true);
                waterAnimator.SetBool("FALLING", false);
                waterAnimator.SetBool("RUNNING", false);
                waterAnimator.SetBool("IDLE", false);
            }
            else if (coyoteTime < 0)
            {
                waterAnimator.SetBool("FALLING", true);
                waterAnimator.SetBool("JUMPING", false);
                waterAnimator.SetBool("RUNNING", false);
                waterAnimator.SetBool("IDLE", false);
            }

        }
        else if(isOnFloor && rb2d.velocity.normalized.y < 0.1f)
        {
            waterAnimator.SetBool("FALLING", false);
            waterAnimator.SetBool("JUMPING", false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(pointToCheckFloor.transform.position, fallCheck);
    }
    
}
