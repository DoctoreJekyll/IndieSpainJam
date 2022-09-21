using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImproveMove : MonoBehaviour
{
	private float _moveInput;
	private Rigidbody2D rb2d;
	private SpriteRenderer _spriteRenderer;

	[Header("Run")] 
	public float moveSpeed;
	public float runAcceleration; //Time (approx.) time we want it to take for the player to accelerate from 0 to the runMaxSpeed.
	public float runDecceleration; //Time (approx.) we want it to take for the player to accelerate from runMaxSpeed to 0.
	public float velPower;

	private void Awake()
	{
		rb2d = GetComponent<Rigidbody2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Update()
    {
	    _moveInput = Input.GetAxisRaw("Horizontal");
	    Flip();
    }
	
    private void FixedUpdate()
    {
	    Run();
    }

    private void Run()
    {
	    //Calcula la direccion y la velocidad
	    float targetSpeed = _moveInput * moveSpeed;
		//Diferencia entre vel actual y la deseada
	    float sppedDif = targetSpeed - rb2d.velocity.x;
		//Cambia el ratio de acelerar segun estamos acelerando o decelerando(creo que esta segunda palabra me la he inventado)
	    float accelRate = (Math.Abs(targetSpeed) > 0.01f) ? runAcceleration : runDecceleration;
		//Aplica aceleracion a la speedDif en resumen
	    float movement = (float)(Math.Pow(Math.Abs(sppedDif) * accelRate, velPower) * Math.Sign(sppedDif));
	    
	    rb2d.AddForce(movement * Vector2.right);
    }
    
    //Estos bools creo que ya no se usan más por el nuevo sistema que he implementado pero los dejo por ahora por si acaso
    [HideInInspector] public bool isFacingRigth;
    [HideInInspector] public bool isFacingLeft;
    private void Flip()
    {
	    if (_moveInput > 0)
	    {
		    /*
		    spriteRenderer.flipX = false;
		    isFacingRigth = true;
		    isFacingLeft = false;*/

		    transform.localScale = new Vector3(1f, 1f, 1f);

	    }
	    else if (_moveInput < 0)
	    {
		    /*
		    spriteRenderer.flipX = true;
		    isFacingLeft = true;
		    isFacingRigth = false;*/
            
		    transform.localScale = new Vector3(-1f, 1f, 1f);
	    }
    }
    
}
