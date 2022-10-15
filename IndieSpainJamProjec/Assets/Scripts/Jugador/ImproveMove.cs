using System;
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

	private Animator _animation;
	
	private void Awake()
	{
		rb2d = GetComponent<Rigidbody2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_animation = GetComponent<Animator>();
	}

	private void Update()
    {
	    _moveInput = Input.GetAxisRaw("Horizontal");
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
	    float targetSpeed = _moveInput * moveSpeed;
		//Diferencia entre vel actual y la deseada
	    float sppedDif = targetSpeed - rb2d.velocity.x;
		//Cambia el ratio de acelerar segun estamos acelerando o decelerando(creo que esta segunda palabra me la he inventado)
	    float accelRate = (Math.Abs(targetSpeed) > 0.01f) ? runAcceleration : runDecceleration;
		//Aplica aceleracion a la speedDif en resumen
	    float movement = (float)(Math.Pow(Math.Abs(sppedDif) * accelRate, velPower) * Math.Sign(sppedDif));
	    
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
    
    private void FlipAndAnimation()
    {
	    if (_moveInput > 0)
	    {
		    transform.localScale = new Vector3(1f, 1f, 1f);
		    _animation.SetBool("isMoving", true);

	    }
	    else if (_moveInput < 0)
	    {
		    transform.localScale = new Vector3(-1f, 1f, 1f);
		    _animation.SetBool("isMoving", true);
	    }
	    else
	    {
		    _animation.SetBool("isMoving", false);
	    }
    }
    
}
