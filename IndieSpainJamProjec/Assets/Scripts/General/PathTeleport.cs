using System;
using System.Collections;
using UnityEngine;

public class PathTeleport : MonoBehaviour
{

    [Header("Transform")]
    [SerializeField] private Transform destination;

    [Header(("Player"))]
    private GameObject playerGO;

    [Header("Sounds")] 
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip goInClip;
    [SerializeField] private AudioClip travelClip;

    enum Points
    {
        POINTA,
        POINTB
    }

    [SerializeField] private Points _points = Points.POINTA;

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
        
        _audioSource.PlayOneShot(goInClip);
        yield return new WaitForSeconds(goInClip.length);
        _audioSource.PlayOneShot(travelClip);
        yield return new WaitForSeconds(travelClip.length);
        _audioSource.PlayOneShot(goInClip);


        if (_points == Points.POINTA)
        {
            Debug.Log("izq");
            Vector3 sumDestination = new Vector3(1, 0f, 0f);
            Vector3 newDestination = destination.position + sumDestination;
            playerGO.transform.position = newDestination;
            
        }else if (_points == Points.POINTB)
        {
            Debug.Log("derecha");
            Vector3 sumDestination = new Vector3(-1f, 0f, 0f);
            Vector3 newDestination = destination.position + sumDestination;
            playerGO.transform.position = newDestination;
            
        }
        


        

        spTemp.enabled = true;
        GameStateManager.instance.currentGameState = GameStateManager.GameState.GAMEPLAY;
    }
    
}
