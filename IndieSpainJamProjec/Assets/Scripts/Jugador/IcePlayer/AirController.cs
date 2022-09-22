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

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        IsOnFloorCheck();
        StopMovementController();
        
    }

    private void StopMovementController()
    {
        playerMoveScript.enabled = isOnFloor;

        if (isOnFloor == false)
        {
            rb2d.velocity = new Vector2(0f, rb2d.velocity.y);
            rb2d.gravityScale += Time.deltaTime * fallSpeed;
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
