using System;
using System.Collections;
using UnityEngine;

public class PathTeleport : MonoBehaviour
{
    public enum TubeExit { UP, DOWN, LEFT, RIGHT }

    [Header("Transform")]
    [SerializeField] private Transform destination;
    public TubeExit tubeExit;

    [Header(("Player"))]
    private GameObject playerGO;

    [Header("Sounds")] 
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip goInClip;
    [SerializeField] private AudioClip travelClip;

    private bool onUse;
    private LevelKey _levelKey;

    enum Points
    {
        POINTA,
        POINTB
    }

    [SerializeField] private Points _points = Points.POINTA;

    private void Awake()
    {
        playerGO = GameObject.FindWithTag("Player");
        _levelKey = FindObjectOfType<LevelKey>();
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
        LevelKeyActivateDesactiva(false);
        _levelKey.gameObject.GetComponentInChildren<ParticleSystem>().Stop();
        
        _audioSource.PlayOneShot(goInClip);
        yield return new WaitForSeconds(goInClip.length);
        _audioSource.PlayOneShot(travelClip);
        yield return new WaitForSeconds(travelClip.length);
        _audioSource.PlayOneShot(goInClip);

        Vector3 sumDestination = Vector3.zero;

        switch (tubeExit)
        {
            case TubeExit.UP:
                sumDestination = new Vector3(0f, 1f, 0f);
                break;
            case TubeExit.DOWN:
                sumDestination = new Vector3(0f, -1f, 0f);
                break;
            case TubeExit.LEFT:
                sumDestination = new Vector3(-1f, 0f, 0f);
                break;
            case TubeExit.RIGHT:
                sumDestination = new Vector3(1f, 0, 0f);
                break;
        }


        if (_points == Points.POINTA)
        {
            Debug.Log("izq");
            Vector3 newDestination = destination.position + sumDestination;
            playerGO.transform.position = newDestination;
            _levelKey.transform.position = newDestination;

        }else if (_points == Points.POINTB)
        {
            Debug.Log("derecha");
            Vector3 newDestination = destination.position + sumDestination;
            playerGO.transform.position = newDestination;
            _levelKey.transform.position = newDestination;
        }
    

        spTemp.enabled = true;
        LevelKeyActivateDesactiva(true);
        _levelKey.gameObject.GetComponentInChildren<ParticleSystem>().Play();
        GameStateManager.instance.currentGameState = GameStateManager.GameState.GAMEPLAY;
    }

    private void LevelKeyActivateDesactiva(bool desactivateKey)
    {
        if (_levelKey.followingPlayer)
        {
            _levelKey.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = desactivateKey;
        }
    }
    
    
}
