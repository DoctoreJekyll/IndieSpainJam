using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase que se encarga de controlar la muerte del jugador
public class PlayerDeath : MonoBehaviour
{
    [Header("[References]")]
    public Animator playerAnimator;

    [Header("[Values]")]
    public bool dead;


    [Header("CheckPointValues")]
    private Vector3 playerCheckPointPos;
    private float checkPointTemperature;
    private CheckPoints checkPoints;
    private TempManager tempManager;
    private PlayerCheckPointValues playerCheckPointValues;

    private void Awake()
    {
        tempManager = FindObjectOfType<TempManager>();
    }

    //Posiciono al jugador en los valores recogidos por el trigger del checkpoint y retorno el estado y el bool
    private void ReturnToLastCheckPoint()
    {
        playerCheckPointValues = GetComponentInParent<PlayerCheckPointValues>();
        
        transform.position = playerCheckPointValues.playerCheckPoinPositionValue;
        tempManager.SetTemperature(playerCheckPointValues.checkPointTemperatureValue);
        
        ReturnPlayerValues();
    }

    private void ReturnPlayerValues()
    {
        if (GameStateManager.instance.currentGameState != GameStateManager.GameState.GAMEPLAY)
        {
            GameStateManager.instance.SetGameState(GameStateManager.GameState.GAMEPLAY);
        }

        dead = false;
    }
    

    //Detiene al jugador, reproduce la animaci?n de muerte y le dice al GameController que ha muerto
    public void OnDeath()
    {
        if(dead == false)
        {
            dead = true;
            CinemachineNoise.instance.ShakeCamera(2f,0.5f);
            GameStateManager.instance.SetGameState(GameStateManager.GameState.EVENT);
            //CameraShake.instance.ShakeCamera(CameraShake.ShakeMagnitude.BIG);
            //playerAnimator.Play("DEATH");

            StartCoroutine(Coroutine_OnDeath());

            IEnumerator Coroutine_OnDeath()
            {
                yield return new WaitForSeconds(0.5f);
                TransitionCanvas.instance.Play_ScreenTransition_In();

                yield return new WaitForSeconds(1);
                ReturnToLastCheckPoint();
                TransitionCanvas.instance.Play_ScreenTransition_Out();
            }
        }
    }

}
