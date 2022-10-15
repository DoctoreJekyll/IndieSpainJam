using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase que se encarga del movimiento del jugador en el estado gaseoso,
//el cual se eleva constantemente y solo puede moverse ligeramente en horizontal
public class Player_GasController : MonoBehaviour
{
    [Header("[References]")]
    public Rigidbody2D rb;

    [Header("[Configuration]")]
    public float elevationForce;
    public float moveSpeed;
    private SpriteRenderer _spriteRenderer;

    [Header("Sounds")] 
    private GasPlayerSound _gasPlayerSound;


    private void Awake()
    {
        _gasPlayerSound = GetComponent<GasPlayerSound>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (GameStateManager.instance.currentGameState == GameStateManager.GameState.GAMEPLAY)
        {
            rb.velocity = Vector3.up * elevationForce;
            PlayerMovement();
        } 
    }

    private void PlayerMovement()
    {
        float inputMovement = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(inputMovement * moveSpeed, rb.velocity.y);

        if (inputMovement > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (inputMovement < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }
}
