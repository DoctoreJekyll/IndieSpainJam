using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb2d;
    
    [Header("Jump Stuffs")]
    [SerializeField] private float jumpForce;
    private bool canJump;
    public bool isOnFloor;
    [SerializeField] private GameObject pointToCheckFloor;
    [SerializeField] private Vector2 boxCheckSize;
    [SerializeField] private LayerMask floorLayer;
    [SerializeField] private GameObject shadow;

    [Header("Coyote Bro")]
    [SerializeField]private float coyoteTime;
    [SerializeField]private float timeToDoCoyote;

    private WaterPlayerSounds _waterPlayerSounds;

    private void Start()
    {
        _waterPlayerSounds = GetComponent<WaterPlayerSounds>();
    }

    private void Update()
    {
        
        IsOnFloor();
        FallCheck();
        
        shadow.SetActive(isOnFloor);
        
        if (GameStateManager.instance.currentGameState == GameStateManager.GameState.GAMEPLAY)
        {
            CoyoteTimeImprove();
            Jump();
        }

    }

    private void IsOnFloor()
    {
        isOnFloor = Physics2D.OverlapBox(pointToCheckFloor.transform.position, boxCheckSize, 0, floorLayer);
    }
    
    private void Jump()
    {
        
        if (coyoteTime > 0f)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _waterPlayerSounds.JumpSound();
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                rb2d.velocity += Vector2.up * jumpForce;
                
            }

            if (Input.GetKeyUp((KeyCode.Space)))
            {
                coyoteTime = 0f;
            }
        }

    }

    private void CoyoteTimeImprove()
    {
        if (isOnFloor)
        {
            coyoteTime = timeToDoCoyote;
        }
        else
        {
            coyoteTime -= Time.deltaTime;
        }
        
    }

    private bool isOnAir;
    public Vector2 soundFallCheck;

    private void FallCheck()
    {
        if (!isOnFloor)
        {
            isOnAir = true;
        }

        if (isOnAir)
        {
            bool fallSound = Physics2D.OverlapBox(pointToCheckFloor.transform.position, soundFallCheck, 0, floorLayer);
            if (fallSound)
            {
                _waterPlayerSounds.FallSound();
                isOnAir = false;
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(pointToCheckFloor.transform.position, soundFallCheck);
    }
}
