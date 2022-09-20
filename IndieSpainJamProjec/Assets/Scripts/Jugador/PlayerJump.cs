using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb2d;
    
    [Header("Jump Stuffs")]
    public float jumpForce;
    public bool canJump;
    [SerializeField] private bool isOnFloor;
    [SerializeField] private GameObject pointToCheckFloor;
    [SerializeField] private Vector2 boxCheckSize;
    [SerializeField] private LayerMask floorLayer;

    [Header("Coyote Bro")]
    [SerializeField]private float coyoteTime;
    [SerializeField]private float timeToDoCoyote;

    private void Update()
    {
        CoyoteTimeImprove();
        Jump();
    }
    
    private void Jump()
    {
        isOnFloor = Physics2D.OverlapBox(pointToCheckFloor.transform.position, boxCheckSize, 0, floorLayer);

        if (playerCanJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                rb2d.velocity += Vector2.up * jumpForce;
            }
        }

    }


    public bool playerCanJump;
    //Creamos una variable float para marcar el tiempo que tenemos para saltar y la ponemos a 0 cada vez que estmos en el suelo, si NO estamos en el suelo sumamos un contador de tiempo, ese contador ser� el tiempo
    //que tendremos para saltar aunque no estemos en el suelo para dar esa buena sensaci�n de coyotito, ta weno.
    private void CoyoteTimeImprove()
    {
        if (isOnFloor)
        {
            coyoteTime = 0;
        }
        else
        {
            coyoteTime += Time.deltaTime;
        }

        if (isOnFloor || (coyoteTime < timeToDoCoyote))
        {
            playerCanJump = true;
        }
        else
        {
            playerCanJump = false;
        }

    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(pointToCheckFloor.transform.position, boxCheckSize);
    }
}
