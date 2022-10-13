using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private float inputMovement;

    public PlayerJump playerJump;
    public Animator waterAnimator;

    [Header("Movement Stuff")]
    public float moveSpeed;
    public float maxMoveSpeed;
    public float moveSpeedWhenSpikes;
    
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>(); 
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (GameStateManager.instance.currentGameState == GameStateManager.GameState.GAMEPLAY)//Controlamos que no puedas hacer flip en estados que no sean gameplay
        {
            Flip();
        }
        
    }

    private void FixedUpdate()
    {
        MoveAvailable();
    }

    private void PlayerMovement()
    {
        inputMovement = Input.GetAxisRaw("Horizontal");
        rb2d.velocity = new Vector2(inputMovement * moveSpeed, rb2d.velocity.y);
        
        AnimationMovement(inputMovement);//Metodo para controlar las animaciones del movimiento
    }

    private void MoveAvailable()
    {
        if (GameStateManager.instance.currentGameState == GameStateManager.GameState.GAMEPLAY)
        {
            PlayerMovement();
        }
        else
        {
            rb2d.velocity = new Vector2(0f, rb2d.velocity.y);//Ñapa porque a veces si saltas encima del teleport e igual para otros eventos el pj sigue con su velocidad, esto lo soluciona por ahora
        }
    }
    
    private void Flip()
    {
        if (inputMovement > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);

        }
        else if (inputMovement < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
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
