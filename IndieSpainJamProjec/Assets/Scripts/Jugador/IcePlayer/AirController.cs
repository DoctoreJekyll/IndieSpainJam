using System;
using UnityEngine;

public class AirController : MonoBehaviour
{

    [Header("Scripts")]
    [SerializeField] private MonoBehaviour playerMoveScript;
    [Header("CheckGround")]
    [SerializeField] private GameObject pointToCheckFloor;
    [SerializeField] private Vector2 boxCheckSize;
    [SerializeField] private LayerMask floorLayer;
    
    [SerializeField] public bool isOnFloor;
    private bool isOnAir;
    private Rigidbody2D rb2d;
    [SerializeField] private float fallSpeed;
    [SerializeField] private GameObject shadow;

    private IcePlayerSounds _icePlayerSounds;
    private Animator _animator;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        _icePlayerSounds = GetComponent<IcePlayerSounds>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        shadow.SetActive(isOnFloor);
        
        IsOnFloorCheck();
        StopMovementController();
        CheckIfPlayerIsOnAir();

    }

    private void StopMovementController()
    {
        playerMoveScript.enabled = isOnFloor;

        if (isOnFloor == false)
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            rb2d.velocity += Vector2.up * (Physics2D.gravity.y * (fallSpeed - 1) * Time.deltaTime);
        }
        else
        {
            rb2d.gravityScale = 1f;
        }
    }
    
    private void IsOnFloorCheck()
    {
        isOnFloor = Physics2D.OverlapBox(pointToCheckFloor.transform.position, boxCheckSize, 0, floorLayer);
    }

    
    
    private void CheckIfPlayerIsOnAir()
    {
        if (!isOnFloor)
        {
            _animator.SetBool("isFall", true);
            timeToPush += Time.deltaTime;
            isOnAirAndPush = true;
        }
        else
        {
            _animator.SetBool("isFall", false);
        }
    }

    private bool isOnAirAndPush;
    private float timeToPush;
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        
        if ((col.gameObject.layer == 10 || col.gameObject.layer == 9) && isOnAirAndPush)
        {
            if (timeToPush > 0 && timeToPush < 0.5f)
            {
                CameraShake.instance.ShakeCamera(CameraShake.ShakeMagnitude.SMALL);
                //CinemachineNoise.instance.ShakeCamera(1f,0.25f);
                _icePlayerSounds.IceImpactClip();
                Debug.Log("small shake");
            }
            else if (timeToPush > 0.5f)
            {
                CameraShake.instance.ShakeCamera(CameraShake.ShakeMagnitude.MEDIUM);
                //CinemachineNoise.instance.ShakeCamera(1.25f,0.5f);
                _icePlayerSounds.IceImpactClip();
                Debug.Log("medium shake");
            }
            
            isOnAirAndPush = false;
            timeToPush = 0f;
        }

    }

    public bool IsOnAir()
    {
        return isOnFloor;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(pointToCheckFloor.transform.position, boxCheckSize);
    }
    
}
