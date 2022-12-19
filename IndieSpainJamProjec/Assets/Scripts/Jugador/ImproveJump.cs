using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;

public class ImproveJump : MonoBehaviour
{
    [Header("References")]
    private Rigidbody2D rb;
    private PlayerJump playerJump;
    
    [Header("Configuration")]
    [SerializeField] private float fallMultiplier = 2.5f;//Cae más rápido después del salto
    [SerializeField] private float lowJumpDivide = 2f;
    [SerializeField] private float gravity;
    
    public bool isOnWindArea;
    public bool isInEspecialTile;
    
    void Start()
    {
        playerJump = GetComponent<PlayerJump>();
        rb = GetComponent<Rigidbody2D>();
        isOnWindArea = false;
    }

    private void FixedUpdate()
    {
        BetterJumpPerformed();
    }

    private void BetterJumpPerformed()
    {
        if (playerJump.isOnFloor || isOnWindArea)
        {
            rb.gravityScale = 1;
            rb.drag = 0;
        }
        else
        {
            rb.gravityScale = gravity;
            rb.drag = 0.25f;
            if (GamepadIsConnected())
            {
                if (rb.velocity.y < 0)
                {
                    rb.gravityScale = gravity * fallMultiplier;
                }
                else if (rb.velocity.y > 0 && !GamepadButtonSouthIsPush() && !KeyBoardButtonSpaceIsPush())
                {
                    rb.gravityScale = gravity * (fallMultiplier / lowJumpDivide);
                }
            }
            else
            {
                if (rb.velocity.y < 0)
                {
                    rb.gravityScale = gravity * fallMultiplier;
                }
                else if (rb.velocity.y > 0 && !KeyBoardButtonSpaceIsPush())
                {
                    rb.gravityScale = gravity * (fallMultiplier / lowJumpDivide);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hot") || other.gameObject.CompareTag("Frost"))
        {
            isInEspecialTile = true;
        }
    }

    private bool GamepadIsConnected() => Gamepad.all.Count > 0;
    private bool GamepadButtonSouthIsPush() => Gamepad.current.buttonSouth.isPressed;
    private bool KeyBoardButtonSpaceIsPush() => Keyboard.current.spaceKey.isPressed;
}
