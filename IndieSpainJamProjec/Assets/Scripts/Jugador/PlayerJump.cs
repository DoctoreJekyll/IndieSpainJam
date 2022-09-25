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

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        
        IsOnFloor();
        
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
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(pointToCheckFloor.transform.position, boxCheckSize);
    }
}
