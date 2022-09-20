using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase que se encarga de comprobar si el jugador ha conseguido la llave para completar el nivel
public class TargetDoor : MonoBehaviour
{
    [Header("[References]")]
    public GameObject player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            CheckPlayerKey();
    }

    private void CheckPlayerKey()
    {
        if(player.GetComponent<PlayerKeyChecker>().Check_PlayerHasKey() == true)
            LevelCompleted();
    }

    private void LevelCompleted()
    {
        GameStateManager.instance.SetGameState(GameStateManager.GameState.EVENT);
        Debug.Log("Level Completed");
    }
}
