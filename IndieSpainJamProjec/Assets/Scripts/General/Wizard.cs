using UnityEngine;

//Clase que se encarga de lanzar proyectiles de fuego o hielo constantemente dependiendo del tipo de mago
public class Wizard : MonoBehaviour
{
    public enum WizardElement { ICE, WATER, FIRE, ARCHER }

    [Header("[References]")]
    private GameObject player;
    public GameObject iceBallPrefab;
    public GameObject waterBallPrefab;
    public GameObject fireBallPrefab;
    public GameObject arrowPrefab;

    [Header("[Configuration]")]
    public WizardElement wizardType;
    public float fireRate, projectileSpeed;
    public bool looksAtPlayer;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        //Dispara proyectiles de forma constante
        InvokeRepeating(nameof(Fire_Projectile), 1, fireRate);
    }


    private void Update()
    {
        //El mago dispara siempre en dirección al jugador
        if (looksAtPlayer && player != null)
        {
            if(player.transform.position.x <= gameObject.transform.position.x)
                gameObject.transform.localScale = new Vector2(-1, 1);
            else
                gameObject.transform.localScale = new Vector2(1, 1);
        }
    }


    //Lanza un proyectil dependiendo del tipo de mago 
    private void Fire_Projectile()
    {
        GameObject newProjectile = null;

        switch (wizardType)
        {
            case WizardElement.ICE:
                newProjectile = Instantiate(iceBallPrefab, transform.position, Quaternion.identity);
                break;
            case WizardElement.WATER:
                newProjectile = Instantiate(waterBallPrefab, transform.position, Quaternion.identity);
                break;
            case WizardElement.FIRE:
                newProjectile = Instantiate(fireBallPrefab, transform.position, Quaternion.identity);
                break;
            case WizardElement.ARCHER:
                newProjectile = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                break;
        }

        newProjectile.GetComponent<Wizard_Projectile>().horizontalDirection = (int)gameObject.transform.localScale.x;
        newProjectile.GetComponent<Wizard_Projectile>().movementSpeed = projectileSpeed;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerDeath playerDeath = col.gameObject.GetComponent<PlayerDeath>();
            playerDeath.OnDeath();
        }
    }
}
