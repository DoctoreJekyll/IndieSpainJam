using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private float gravityScale;
    
    [Header("Jump Stuffs")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCutMultiplier;
    [SerializeField] private float fallGravityMultiply;
    [SerializeField] private GameObject pointToCheckFloor;
    [SerializeField] private Vector2 boxCheckSize;
    [SerializeField] private LayerMask floorLayer;
    [SerializeField] private GameObject shadow;
    [SerializeField] private TrailRenderer trail;
    private bool canJump;
    public bool isOnFloor;

    [Header("Fall Suffs")]
    [SerializeField] private bool isOnAir;
    public Vector2 fallCheck;

    [Header("Coyote Bro")]
    [SerializeField]private float coyoteTime;
    [SerializeField]private float timeToDoCoyote;

    private WaterPlayerSounds _waterPlayerSounds;
    private Animator waterAnimator;

    [Header("Particles")]
    [SerializeField] private ParticleSystem fallParticle;

    private void Start()
    {
        waterAnimator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        _waterPlayerSounds = GetComponent<WaterPlayerSounds>();
        if(trail) { trail.gameObject.SetActive(false); }

        gravityScale = rb2d.gravityScale;
    }

    private void Update()
    {
        
        IsOnFloor();
        FallCheck();
        CoyoteTimeImprove();

        shadow.SetActive(isOnFloor);
    }

    private void IsOnFloor()
    {
        isOnFloor = Physics2D.OverlapBox(pointToCheckFloor.transform.position, boxCheckSize, 0, floorLayer);
        JumpControllerAnim();
    }

    public void JumpAction(InputAction.CallbackContext context)//Llamamos a este metodo dentro del componente input action del playermanager 
    {
        if (GameStateManager.instance.currentGameState == GameStateManager.GameState.GAMEPLAY)
        {
            if (context.performed)
            {
                if (coyoteTime > 0f)
                {
                    JumpMethod();

                    if (context.canceled)
                    {
                        coyoteTime = 0f;
                    }
                }
            }
            
        }
    }

    private void JumpMethod()
    {
        if (!isOnFloor)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x,0f);
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        else
        {
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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
            }

        }
    }

    private void JumpControllerAnim()//Método que se encarga de controlar las anim
    {
        if(isOnFloor == false)
        {
            trail.gameObject.SetActive(true);
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
        else
        {
            if (trail) { trail.gameObject.SetActive(false); }
            waterAnimator.SetBool("FALLING", false);
            waterAnimator.SetBool("JUMPING", false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(pointToCheckFloor.transform.position, fallCheck);
    }
    
    private void OldJumpMethod()
    {
        _waterPlayerSounds.JumpSound();
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        rb2d.velocity += Vector2.up * jumpForce;
    }
}
