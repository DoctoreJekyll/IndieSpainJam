using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase que se encarga de mover el proyectil de forma constante y de comprobar
//si ha impactado con algo y actuar en consecuencia
public class Wizard_Projectile : MonoBehaviour
{
    public enum ProjectileElement { ICE, WATER, FIRE }

    [Header("[References]")]
    private TempManager tempManager;
    private PlayerStatesManager playerState;
    private GameObject player;
    public Rigidbody2D rb;

    [Header("[Configuration]")]
    public ProjectileElement projectileElement;

    [Header("[Values]")]
    public float speed;
    public int horizontalDirection;


    //Obtenemos los componentes necesarios y cambiamos la escala en X al proyectil  hacia la dirección a la que va
    private void Start()
    {
        tempManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<TempManager>();
        playerState = GameObject.FindGameObjectWithTag("Player Controller").GetComponent<PlayerStatesManager>();
        player = GameObject.FindGameObjectWithTag("Player");

        gameObject.transform.localScale = new Vector2(horizontalDirection, 1);
    }

    //Movemos el proyectil de forma constante
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * horizontalDirection, 0);
    }


    //Si detecta una colisión, aplicamos la temperatura si es el jugador y destruimos el proyectil
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ImpactPlayer();
            DestroyProjectile();
        }
        else
        {
            DestroyProjectile();
        }
    }


    //Al impactar al jugador le aplicamos temperatura o lo empujamos
    private void ImpactPlayer()
    {
        //Si el elemento del proyectil y el jugador son el mismo, empuja al jugador
        if((int) projectileElement == (int) playerState.currentPlayerState)
            PushPlayer();

        else //Si son elementos distintos, le cambia al siguiente estado
            SwapPlayerState();
    }


    //El proyectil empuja al jugador
    private void PushPlayer()
    {
        //Codigo de empujar al jugador en la direccion a la que iba el proyectil
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
        }
    }


    //Se destruye el proyectil
    private void DestroyProjectile()
    {
        //(TODO) Que se instancie particulas de su elemento al impactar
        Destroy(gameObject);
    }
}
