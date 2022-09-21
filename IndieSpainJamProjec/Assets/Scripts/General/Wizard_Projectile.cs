using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_Projectile : MonoBehaviour
{
    public enum ProjectileType { FIRE, ICE }

    [Header("[References]")]
    public Rigidbody2D rb;

    [Header("[Configuration]")]
    public ProjectileType projectileType;
    public float projectileTemperature;

    [Header("[Values]")]
    public float speed;
    public int horizontalDirection;


    private void Start()
    {
        gameObject.transform.localScale = new Vector2(horizontalDirection, 1);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * horizontalDirection, 0);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Aplico temperatura al jugador");
            //DestroyProjectile();
        }
        else
        {
            //DestroyProjectile();
        }
    }


    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
