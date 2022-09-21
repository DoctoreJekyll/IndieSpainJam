using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase que se encarga de lanzar proyectiles de fuego o hielo constantemente
//dependiendo del tipo de mago
public class Wizard : MonoBehaviour
{
    public enum WizardType { FIRE, ICE }

    [Header("[References]")]
    private GameObject player;
    public GameObject fireBallPrefab;
    public GameObject iceBallPrefab;

    [Header("[Configuration]")]
    public WizardType wizardType;
    public float fireRate, projectileSpeed;
    public bool looksAtPlayer;


    private void Start()
    {
        InvokeRepeating(nameof(Fire_Projectile), 1, fireRate);

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (looksAtPlayer && player != null)
        {
            if(player.transform.position.x <= gameObject.transform.position.x)
                gameObject.transform.localScale = new Vector2(-1, 1);
            else
                gameObject.transform.localScale = new Vector2(1, 1);
        }
    }

    private void Fire_Projectile()
    {
        GameObject newProjectile;

        if (wizardType == WizardType.FIRE)
            newProjectile = Instantiate(fireBallPrefab, transform.position, Quaternion.identity);
        else
            newProjectile = Instantiate(iceBallPrefab, transform.position, Quaternion.identity);


        newProjectile.GetComponent<Wizard_Projectile>().horizontalDirection = (int) gameObject.transform.localScale.x;
        newProjectile.GetComponent<Wizard_Projectile>().speed = projectileSpeed;
    }

}
