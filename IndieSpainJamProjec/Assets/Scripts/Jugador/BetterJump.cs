using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BetterJump : MonoBehaviour
{
    private Rigidbody2D rb;
    public float fallMultiplier = 2.5f;//Cae más rápido después del salto
    public float lowJumpMultiplier = 2f;//Salto minimo
    private float gravityScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravityScale = rb.gravityScale;
    }

    private void Update()
    {
         BetterJumpPerformed();
    }

    private void BetterJumpPerformed()
    {

        if (Gamepad.all.Count > 0)
        {
            
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * (Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
                //rb.gravityScale = gravityScale * fallMultiplier;
            }
            else if (rb.velocity.y > 0 && !Gamepad.current.buttonSouth.isPressed && !Keyboard.current.spaceKey.isPressed)
            {
                rb.velocity += Vector2.up * (Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime);
                //rb.gravityScale = gravityScale * fallMultiplier;
            }
            // else
            // {
            //     rb.gravityScale = gravityScale;
            // }
        }
        else
        {
            
            if (rb.velocity.y < 0)
            {
                //rb.gravityScale = gravityScale * fallMultiplier;
                rb.velocity += Vector2.up * (Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
            }
            else if (rb.velocity.y > 0 && !Keyboard.current.spaceKey.isPressed)
            {
                rb.velocity += Vector2.up * (Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime);
                //rb.gravityScale = gravityScale * fallMultiplier;
            }
            // else
            // {
            //     rb.gravityScale = gravityScale;
            // }
        }

    }
    
    private void OnJumpUp()
    {
        if (rb.velocity.y > 0)
        {
            rb.AddForce(Vector2.down * rb.velocity.y * (1- lowJumpMultiplier), ForceMode2D.Impulse);
        }
    }

    private void JumpGravity()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = gravityScale * fallMultiplier;
        }
        else
        {
            rb.gravityScale = gravityScale;
        }
    }
    
    
}
