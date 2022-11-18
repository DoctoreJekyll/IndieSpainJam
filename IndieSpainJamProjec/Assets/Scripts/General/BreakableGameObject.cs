using UnityEngine;

public class BreakableGameObject : MonoBehaviour
{
    
    private RaycastHit2D hitInfo;
    private RaycastHit2D hitInfoLeft;
    private RaycastHit2D hitInfoRight;

    [Header("Layer")]
    [SerializeField] private LayerMask layerWhoBrokeTheObj;
    
    [Header("Ray Range")]
    [SerializeField] private float range;
    [SerializeField] private float distanceRay0;
    [SerializeField] private float distanceRay1;
    [SerializeField] private float distanceRay2;

    [Header("Sprites Stuffs")]
    [SerializeField] private SpriteRenderer breakableSpriteRenderLeft;
    [SerializeField] private SpriteRenderer breakableSpriteRenderRigth;
    [SerializeField] private Sprite breakSpriteLeft;
    [SerializeField] private Sprite breakSpriteRigth;

    [Header("Collider")]
    [SerializeField] private Collider2D collider;

    //Other stuffs
    private AudioSource _audioSource;//Generar aqui el sonido no me convence 
    private bool notBreak;

    private void Start()
    {
        _isplayerAirControllerNull = playerAirController == null;
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        notBreak = true;
    }

    //TE RECORDAREMOS CON ODIO E IRA: Aquí había un script al que recordaremos con odio e ira, dejemos este comentario como memorándum

    private void Update()
    {
        CheckIfIcePlayerIsOn();
        BreakGround();
    }
    
    private bool CheckIfIcePlayerIsOn()
    {
        Vector2 rayDir = new Vector2(0, 0.5f);//Direccion de los rayos
        Vector2 rayOrigin = new Vector2(transform.position.x + distanceRay0, transform.position.y);
        Vector2 rayOriginLeft = new Vector2(transform.position.x + distanceRay1, transform.position.y);
        Vector2 rayOriginRigth = new Vector2(transform.position.x + distanceRay2, transform.position.y);
        
        hitInfo = Physics2D.Raycast(rayOrigin, rayDir, range, layerWhoBrokeTheObj);
        hitInfoLeft = Physics2D.Raycast(rayOriginLeft, rayDir, range, layerWhoBrokeTheObj);
        hitInfoRight = Physics2D.Raycast(rayOriginRigth, rayDir, range, layerWhoBrokeTheObj);
        Color rayColor = Color.green;

        if (hitInfo == true || hitInfoLeft == true || hitInfoRight == true)
        {
            return true;
        }
    
        Debug.DrawRay(rayOrigin, rayDir * range, rayColor);
        Debug.DrawRay(rayOriginLeft, rayDir * range, rayColor);
        Debug.DrawRay(rayOriginRigth, rayDir * range, rayColor);
        return false;
    }


    private AirController playerAirController;
    private bool _isplayerAirControllerNull;

    private void BreakGround() //Si el jugador está en el suelo y el suelo no esta roto hacemos cositas
    {
        if (_isplayerAirControllerNull)
        {
            playerAirController = FindObjectOfType<AirController>();
        }
        
        if (CheckIfIcePlayerIsOn() && playerAirController.isOnAirAndPush)
        {
            if (notBreak)
            {
                _audioSource.PlayOneShot(_audioSource.clip);
                notBreak = false;
            }
            //CameraShake.instance.ShakeCamera(CameraShake.ShakeMagnitude.SMALL);
            CinemachineNoise.instance.ShakeCamera(1f,0.25f);
            breakableSpriteRenderLeft.sprite = breakSpriteLeft;
            breakableSpriteRenderRigth.sprite = breakSpriteRigth;
            collider.enabled = false;
        }
    }
    
}
