using System;
using UnityEngine;

public class Player_SolidController : MonoBehaviour
{
	private Vector2 direction;
	private Rigidbody2D playerRb;
	private SpriteRenderer _spriteRenderer;

	[Header("[Movement]")] 
	public float moveSpeed;
	public float runAcceleration; //Time (approx.) time we want it to take for the player to accelerate from 0 to the runMaxSpeed.
	public float runDecceleration; //Time (approx.) we want it to take for the player to accelerate from runMaxSpeed to 0.
	public float velPower;
	
	[Header("Input Controller")]
	private HydroMorpher playerInputsActions;
	private Animator _animation;
	


	private void Awake()
	{
		playerRb = GetComponent<Rigidbody2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_animation = GetComponent<Animator>();
		
		SetNewPlayerInput();
	}

	//Establecemos el Input System
	private void SetNewPlayerInput()
	{
		playerInputsActions = new HydroMorpher();
		playerInputsActions.Enable();
	}


	//Mientras pueda jugarse libremente, dejamos al jugador moverse
	private void FixedUpdate()
    {
		if (GameStateManager.instance.currentGameState == GameStateManager.GameState.GAMEPLAY)
		{
			PlayerMovement();
		}
		else
		{
			playerRb.velocity = new Vector2(0f, playerRb.velocity.y);//Ñapa temporal
		}
    }


	//Obtenemos el input de movimiento y movemos al jugador
    private void PlayerMovement()
    {
        direction = playerInputsActions.PlayerInputs.Move.ReadValue<Vector2>();
        float inputMovement = direction.x;

        //Calcula la direccion y la velocidad
        float targetSpeed = inputMovement * moveSpeed;
		//Diferencia entre vel actual y la deseada
	    float sppedDif = targetSpeed - playerRb.velocity.x;
		//Cambia el ratio de acelerar segun estamos acelerando o decelerando(creo que esta segunda palabra me la he inventado)
	    float accelRate = (Math.Abs(targetSpeed) > 0.01f) ? runAcceleration : runDecceleration;
		//Aplica aceleracion a la speedDif en resumen
	    float movement = (float)(Math.Pow(Math.Abs(sppedDif) * accelRate, velPower) * Math.Sign(sppedDif));
	    
	    playerRb.AddForce(movement * Vector2.right);

		FlipSprite(inputMovement);
		FlipAndAnimation(inputMovement);
    }


	//Modificamos el sprite del jugador cuando se mueva en otra dirección
	private void FlipSprite(float inputMovement)
    {
		if(inputMovement > 0)
			transform.localScale = new Vector3(1f, 1f, 1f);

		else if(inputMovement < 0)
			transform.localScale = new Vector3(-1f, 1f, 1f);
    }

    
	//Reproducimos la animación de moverse o no según el input del jugador
    private void FlipAndAnimation(float inputMovement)
    {
	    if (inputMovement > 0 || inputMovement < 0)
		    _animation.SetBool("isMoving", true);

	    else
		    _animation.SetBool("isMoving", false);
    }
    
}
