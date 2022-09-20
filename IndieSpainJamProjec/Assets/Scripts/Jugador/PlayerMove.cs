using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private float inputMovement;

    [Header("Movement Stuff")]
    public float moveSpeed;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>(); 
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Flip();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }
    public void PlayerMovement()
    {
        inputMovement = Input.GetAxisRaw("Horizontal");
        rb2d.velocity = new Vector2(inputMovement * moveSpeed, rb2d.velocity.y);
    }

    private void Flip()
    {
        if (inputMovement > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (inputMovement < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
    
}
