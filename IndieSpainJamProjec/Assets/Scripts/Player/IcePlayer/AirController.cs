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

    private void StopMovementController()//Cuando estamos en el aire impedimos al jugador moverse, ponemos su velocidad en X a 0 y aceleramos la fuerza de gravedad para que caiga a mayor velocidad
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
    
    private void IsOnFloorCheck()//Comprobamos si está tocando el suelo
    {
        isOnFloor = Physics2D.OverlapBox(pointToCheckFloor.transform.position, boxCheckSize, 0, floorLayer);
    }

    
    private void CheckIfPlayerIsOnAir()//Comprobamos si no está en el suelo
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
    
    private void OnCollisionEnter2D(Collision2D col)//Si está en el aire, según la distancia recorrida el screenshake es mayor o menor
    {
        if ((col.gameObject.layer == 10 || col.gameObject.layer == 9) && isOnAirAndPush)
        {
            if (timeToPush > 0 && timeToPush < 0.5f)
            {
                CameraShake.instance.ShakeCamera(CameraShake.ShakeMagnitude.SMALL);
                _icePlayerSounds.IceImpactClip();
            }
            else if (timeToPush > 0.5f)
            {
                CameraShake.instance.ShakeCamera(CameraShake.ShakeMagnitude.MEDIUM);
                _icePlayerSounds.IceImpactClip();
            }
            
            isOnAirAndPush = false;
            timeToPush = 0f;
        }

    }

    private void OnDrawGizmos()//Dibujamos gizmos para ver el comprobador del suelo
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(pointToCheckFloor.transform.position, boxCheckSize);
    }
    
}
