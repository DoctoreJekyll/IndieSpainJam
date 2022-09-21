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

    // Update is called once per frame
    private void FixedUpdate()
    {
	    Run();
    }

    private void Run()
    {
	    float targetSpeed = _moveInput * moveSpeed;

	    float sppedDif = targetSpeed - rb2d.velocity.x;

	    float accelRate = (Math.Abs(targetSpeed) > 0.01f) ? runAcceleration : runDecceleration;

	    float movement = (float)(Math.Pow(Math.Abs(sppedDif) * accelRate, velPower) * Math.Sign(sppedDif));
	    
	    rb2d.AddForce(movement * Vector2.right);
    }
    
    [HideInInspector] public bool isFacingRigth;
    [HideInInspector] public bool isFacingLeft;
    private void Flip()
    {
	    if (_moveInput > 0)
	    {
		    _spriteRenderer.flipX = false;
		    isFacingRigth = true;
		    isFacingLeft = false;
	    }
	    else if (_moveInput < 0)
	    {
		    _spriteRenderer.flipX = true;
		    isFacingLeft = true;
		    isFacingRigth = false;
	    }
    }
    
}
