using System;
using System.Collections;
using UnityEngine;

public class PathTeleport : MonoBehaviour
{

    [Header("Transform")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [Header(("Player"))]
    private GameObject playerGO;

    private void Awake()
    {
        playerGO = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            StartCoroutine(MovePlayerToOterPos());
        }
    }

    private IEnumerator MovePlayerToOterPos()
    {
        GameStateManager.instance.currentGameState = GameStateManager.GameState.EVENT;//Cambia el estado de juego a evento
        SpriteRenderer spTemp = playerGO.GetComponent<SpriteRenderer>();//Pillo el sprite del jugador
        spTemp.enabled = false;
        yield return new WaitForSeconds(2f);
        playerGO.transform.position = pointB.position;//Muevo el jugador a la posicion B
        spTemp.enabled = true;
        GameStateManager.instance.currentGameState = GameStateManager.GameState.GAMEPLAY;
    }
    
}
