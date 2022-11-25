using System;
using UnityEngine;

//Clase que se encarga del movimiento del jugador en el estado gaseoso,
//el cual se eleva constantemente y solo puede moverse ligeramente en horizontal
public class Player_GasController : MonoBehaviour
{
    [Header("[References]")]
    private SpriteRenderer _spriteRenderer;
    public Rigidbody2D playerRb;
    private bool isFacinRigth;

    [Header("[Configuration]")]
    public float elevationForce;
    public float elevationMaxSpeed;
    public float moveSpeed;
    public float runAcceleration;
    public float runDecceleration;
    public float velPower;
    public float lerpAmount;
    
    [Header("Input Controller")]
    private HydroMorpher playerInputsActions;
    private Vector2 direction;


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetNewPlayerInput();
    }
    
    private void OnEnable()
    {
        SetNewPlayerInput();
    }
	
    private void OnDisable()
    {
        playerInputsActions.Disable();
    }
    
    //Establecemos el Input System
    private void SetNewPlayerInput()
    {
        playerInputsActions = new HydroMorpher();
        playerInputsActions.Enable();
    }


    //Mientras pueda jugarse libremente, dejamos al jugador moverse y elevarse
    private void FixedUpdate()
    {
        if (GameStateManager.instance.currentGameState == GameStateManager.GameState.GAMEPLAY)
        {
            Elevate();
            PlayerMovement();
        } 
    }


    //Elevamos al jugador de forma constante
    private void Elevate()
    {
        playerRb.AddForce(Vector3.up * elevationForce);

        if (playerRb.velocity.y > elevationMaxSpeed)
            playerRb.velocity = new Vector2(playerRb.velocity.x, elevationMaxSpeed);
    }


    //Obtenemos el input de movimiento y movemos al jugador
    private void PlayerMovement()
    {
        direction = playerInputsActions.PlayerInputs.Move.ReadValue<Vector2>().normalized;
        float inputMovement = direction.x;

        //Calcula la direccion y la velocidad
        float targetSpeed = inputMovement * moveSpeed;
        targetSpeed = Mathf.Lerp(playerRb.velocity.x, targetSpeed, lerpAmount);
        //Diferencia entre vel actual y la deseada
        float sppedDif = targetSpeed - playerRb.velocity.x;
        //Cambia el ratio de acelerar segun estamos acelerando o decelerando(creo que esta segunda palabra me la he inventado)
        float accelRate = (Math.Abs(targetSpeed) > 0.01f) ? runAcceleration : runDecceleration;
        //Aplica aceleracion a la speedDif en resumen
        float movement = (float)(Math.Pow(Math.Abs(sppedDif) * accelRate, velPower) * Math.Sign(sppedDif));
        //float movement = sppedDif * accelRate;
        
        playerRb.AddForce(movement * Vector2.right);

        FlipSprite(inputMovement);
    }

    
    private void FlipFunction()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        isFacinRigth = !isFacinRigth;
    }
    

    //Modificamos el sprite del jugador cuando se mueva en otra dirección
    private void FlipSprite(float inputMovement)
    {
        if (inputMovement > 0)
            transform.localScale = new Vector3(1f, 1f, 1f);

        else if (inputMovement < 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);
    }
}
