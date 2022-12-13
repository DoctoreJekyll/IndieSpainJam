using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ImproveJump : MonoBehaviour
{

    [SerializeField] private float gravity;
    [SerializeField] private float fallMultiply;

    private void Start()
    {
        Debug.Log("test");
    }

    public void ModifyGravity(bool isOnFloor, Rigidbody2D rb2d)
    {
        if (isOnFloor)
        {
            rb2d.gravityScale = 0;
        }
        else
        {
            rb2d.gravityScale = gravity;
            if (rb2d.velocity.y < 0)
            {
                rb2d.gravityScale = gravity * fallMultiply;
            }
            else if (rb2d.velocity.y > 0 && !Input.GetKeyDown(KeyCode.Space))//Añadir si no esta pulsando ningun boton
            {
                rb2d.gravityScale = gravity * (fallMultiply / 2);
            }
        }
    }
    
    private bool GamepadIsConnected() => Gamepad.all.Count > 0;
}
