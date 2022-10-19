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
    
    [Header("Input Controller")]
    private HydroMorpher playerInputsActions;
    private Vector2 direction;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetNewPlayerInput();
    }
    
    private void SetNewPlayerInput()
    {
        playerInputsActions = new HydroMorpher();
        playerInputsActions.Enable();
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
        direction = playerInputsActions.PlayerInputs.Move.ReadValue<Vector2>();
        float inputMovement = direction.x;
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
