using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_GasController : MonoBehaviour
{
    [Header("[References]")]
    public Rigidbody2D rb;

    [Header("[Configuration]")]
    public float elevationForce;
    public float moveSpeed;

    private void FixedUpdate()
    {
        rb.velocity = Vector3.up * elevationForce;
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float inputMovement = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(inputMovement * moveSpeed, rb.velocity.y);
    }
}
