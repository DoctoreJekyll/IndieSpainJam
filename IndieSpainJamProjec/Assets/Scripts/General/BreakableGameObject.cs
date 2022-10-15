using UnityEngine;

public class BreakableGameObject : MonoBehaviour
{

    [Header("Layer")]
    [SerializeField] private LayerMask layerWhoBrokeTheObj;
    
    [Header("Ray Range")]
    [SerializeField] private float range;
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
        Vector2 rayDir = new Vector2(0, 0.5f);
        Vector2 rayOrigin = transform.position;
        Vector2 rayOriginLeft = new Vector2(transform.position.x + distanceRay1, transform.position.y);
        Vector2 rayOriginRigth = new Vector2(transform.position.x + distanceRay2, transform.position.y);
        
        RaycastHit2D hitInfo = Physics2D.Raycast(rayOrigin, rayDir, range, layerWhoBrokeTheObj);
        RaycastHit2D hitInfoLeft = Physics2D.Raycast(rayOriginLeft, rayDir, range, layerWhoBrokeTheObj);
        RaycastHit2D hitInfoRight = Physics2D.Raycast(rayOriginRigth, rayDir, range, layerWhoBrokeTheObj);
        Color rayColor = Color.green;
        
        if (hitInfo == true || hitInfoLeft == true || hitInfoRight == true)
        {
            return true;
        }
    
        Debug.DrawRay(transform.position, rayDir * range, rayColor);
        Debug.DrawRay(rayOriginLeft, rayDir * range, rayColor);
        Debug.DrawRay(rayOriginRigth, rayDir * range, rayColor);
        return false;
    }

    private void BreakGround() //Si el jugador está en el suelo y el suelo no esta roto hacemos cositas
    {
        if (CheckIfIcePlayerIsOn())
        {
            if (notBreak)
            {
                _audioSource.PlayOneShot(_audioSource.clip);
                notBreak = false;
            }
            CameraShake.instance.ShakeCamera(CameraShake.ShakeMagnitude.SMALL);
            breakableSpriteRenderLeft.sprite = breakSpriteLeft;
            breakableSpriteRenderRigth.sprite = breakSpriteRigth;
            collider.enabled = false;
        }
    }
    
}
