using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Esta clase se encarga de el estado actual de la partida, controlando como se comporta
//el jugador y el resto de la escena en consecuencia
public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
    public enum GameState { NULL, GAMEPLAY, PAUSE, EVENT }

    [Header("[Values]")]
    public GameState currentGameState;


    private void Awake()
    {
        CreateSingleton();
        currentGameState = GameState.NULL;
    }
    private void CreateSingleton()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }


    //Cambia el estado actual de la partida por otro nuevo
    public void SetGameState(GameState newGameState)
    {
        switch (newGameState)
        {
            case GameState.NULL:
                currentGameState = GameState.NULL;
                break;

            case GameState.GAMEPLAY:
                currentGameState = GameState.GAMEPLAY;
                Time.timeScale = 1;
                break;

            case GameState.PAUSE:
                currentGameState = GameState.PAUSE;
                Time.timeScale = 0;
                break;

            case GameState.EVENT:
                currentGameState = GameState.EVENT;
                break;
        }
    }
}
