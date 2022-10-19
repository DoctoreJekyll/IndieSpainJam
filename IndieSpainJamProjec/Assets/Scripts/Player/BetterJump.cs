using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BetterJump : MonoBehaviour
{
    private Rigidbody2D rb;
    public float fallMultiplier = 2.5f;//Cae más rápido después del salto
    public float lowJumpMultiplier = 2f;//"Flota" más en el aire o se mantiene un poco mas

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        BetterJumpPerformed();
    }

    private void BetterJumpPerformed()
    {

        if (Gamepad.all.Count > 0)
        {
            Debug.Log("Hay gamepad");
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * (Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
            }
            else if (rb.velocity.y > 0 && !Gamepad.current.buttonSouth.isPressed && !Keyboard.current.spaceKey.isPressed)
            {
                rb.velocity += Vector2.up * (Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime);
            }
        }
        else
        {
            Debug.Log("No hay gamepad");
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * (Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
            }
            else if (rb.velocity.y > 0 && !Keyboard.current.spaceKey.isPressed)
            {
                rb.velocity += Vector2.up * (Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime);
            }
        }

    }
    
    
}
