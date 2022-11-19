using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase que se encarga de mover el proyectil de forma constante y de comprobar
//si ha impactado con algo y actuar en consecuencia
public class Wizard_Projectile : MonoBehaviour
{
    public enum ProjectileElement { ICE, WATER, FIRE, ARROW }

    [Header("[References]")]
    private TempManager tempManager;
    private PlayerStatesManager playerState;
    public Rigidbody2D rb, playerRb;

    [Header("[Configuration]")]
    public ProjectileElement projectileElement;
    public float pushForce, elevationForce;

    [Header("[Values]")]
    [HideInInspector] public float movementSpeed;
    [HideInInspector] public int horizontalDirection;


    //Obtenemos los componentes necesarios y cambiamos la escala en X al proyectil  hacia la dirección a la que va
    private void Start()
    {
        tempManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<TempManager>();
        playerState = GameObject.FindGameObjectWithTag("Player Controller").GetComponent<PlayerStatesManager>();

        gameObject.transform.localScale = new Vector2(horizontalDirection, 1);
    }


    //Movemos el proyectil de forma constante
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movementSpeed * horizontalDirection, 0);
    }


    //Si detecta una colisión, aplicamos la temperatura si es el jugador y destruimos el proyectil
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ImpactPlayer(collision.gameObject.GetComponent<Rigidbody2D>());
            DestroyProjectile();
        }
        else
        {
            DestroyProjectile();
        }
    }


    //Al impactar al jugador le aplicamos temperatura o lo empujamos
    private void ImpactPlayer(Rigidbody2D playerRb)
    {
        //Si el elemento del proyectil y el jugador son el mismo, empuja al jugador
        if((int) projectileElement == (int) playerState.currentPlayerState)
            PushPlayer(playerRb);

        else //Si son elementos distintos, le cambia al siguiente estado
            SwapPlayerState();
    }


    //El proyectil empuja al jugador
    private void PushPlayer(Rigidbody2D playerRb)
    {
        Debug.Log("Empujo al jugador");
        playerRb.AddForce(new Vector2(horizontalDirection * pushForce, elevationForce), ForceMode2D.Impulse);
        CinemachineNoise.instance.ShakeCamera(1f, 0.25f);
    }


    //Dependiendo del elemento del proyectil, cambia el estado al jugador y le establece una nueva temperatura
    private void SwapPlayerState()
    {
        switch (projectileElement)
        {
            case ProjectileElement.ICE:
                if(playerState.currentPlayerState == PlayerStatesManager.PlayerState.LIQUID)
                {
                    playerState.SwitchBetweenPlayers(PlayerStatesManager.PlayerState.SOLID);
                    tempManager.SetTemperature(0);
                }

                else if(playerState.currentPlayerState == PlayerStatesManager.PlayerState.GAS)
                {
                    playerState.SwitchBetweenPlayers(PlayerStatesManager.PlayerState.LIQUID);
                    tempManager.SetTemperature(75);
                }
                break;


            case ProjectileElement.WATER:
                if (playerState.currentPlayerState == PlayerStatesManager.PlayerState.SOLID)
                {
                    playerState.SwitchBetweenPlayers(PlayerStatesManager.PlayerState.LIQUID);
                    tempManager.SetTemperature(25);
                }

                else if(playerState.currentPlayerState == PlayerStatesManager.PlayerState.GAS)
                {
                    playerState.SwitchBetweenPlayers(PlayerStatesManager.PlayerState.LIQUID);
                    tempManager.SetTemperature(75);
                }
                break;


            case ProjectileElement.FIRE:
                if (playerState.currentPlayerState == PlayerStatesManager.PlayerState.SOLID)
                {
                    playerState.SwitchBetweenPlayers(PlayerStatesManager.PlayerState.LIQUID);
                    tempManager.SetTemperature(25);
                }

                else if (playerState.currentPlayerState == PlayerStatesManager.PlayerState.LIQUID)
                {
                    playerState.SwitchBetweenPlayers(PlayerStatesManager.PlayerState.GAS);
                    tempManager.SetTemperature(100);
                }
                break;


            case ProjectileElement.ARROW:
                if (playerState.currentPlayerState == PlayerStatesManager.PlayerState.SOLID)
                {
                    PlayerDeath playerDeath = playerState.GetComponentInChildren<PlayerDeath>();
                    playerDeath.OnDeath();
                }
                break;
        }
    }


    //Se destruye el proyectil
    private void DestroyProjectile()
    {
        //(TODO) Que se instancie particulas de su elemento al impactar
        Destroy(gameObject);
    }
}
