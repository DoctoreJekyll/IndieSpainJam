using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//Clase que se encarga de mover el jugador a través de la tuberia cuando la toca
public class Pipeline : MonoBehaviour
{
    private Tween currentTween;
    private Vector3[] pathPoints;

    private GameObject player;
    private PlayerStatesManager playerStateManager;
    private Rigidbody2D playerRb;
    private Collider2D playerCollider;
    private SpriteRenderer playerSprite;

    [Header("[Configuration]")]
    public float pathDuration;



    private void Start()
    {
        playerStateManager = GameObject.FindGameObjectWithTag("Player Controller").GetComponent<PlayerStatesManager>();

        pathPoints = new Vector3[gameObject.transform.childCount];

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            pathPoints[i].x = gameObject.transform.GetChild(i).position.x;
            pathPoints[i].y = gameObject.transform.GetChild(i).position.y;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerStateManager.currentPlayerState == PlayerStatesManager.PlayerState.LIQUID)
        {
            if(playerRb == null)
            {
                player = collision.gameObject;
                playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                playerCollider = collision.gameObject.GetComponent<Collider2D>();
                playerSprite = collision.gameObject.GetComponent<SpriteRenderer>();
            }

            PlayPath();
        }
    }


    private void LockPlayer()
    {
        GameStateManager.instance.currentGameState = GameStateManager.GameState.EVENT;
        playerRb.bodyType = RigidbodyType2D.Static;
        playerRb.velocity = Vector2.zero;
        playerCollider.enabled = false;
        playerSprite.enabled = false;
    }


    private void UnlockPlayer()
    {
        GameStateManager.instance.currentGameState = GameStateManager.GameState.GAMEPLAY;
        playerRb.bodyType = RigidbodyType2D.Dynamic;
        playerCollider.enabled = true;
        playerSprite.enabled = true;
    }


    private void PlayPath()
    {
        LockPlayer();

        currentTween.Kill();
        currentTween = player.transform.DOPath(pathPoints, pathDuration, PathType.Linear)
            .SetEase(Ease.Linear)
            .OnComplete(UnlockPlayer);
    }
}
