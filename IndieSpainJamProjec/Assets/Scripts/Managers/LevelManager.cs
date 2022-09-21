using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Clase que se encarga de preparar todo lo necesario en el nivel y de actuar en el
//momento en el que el jugador muere o completa el nivel para cargar el siguiente
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Header("[References]")]
    public GameObject player;
    public GameObject initialDoor;


    private void Awake()
    {
        CreateSingleton();
    }
    private void CreateSingleton()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        PrepareLevel();
    }


    public void PrepareLevel()
    {
        player.transform.position = initialDoor.transform.position;
        GameStateManager.instance.SetGameState(GameStateManager.GameState.GAMEPLAY);
    }

    public void PlayerDeath()
    {
        GameStateManager.instance.SetGameState(GameStateManager.GameState.EVENT);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelCompleted()
    {
        GameStateManager.instance.SetGameState(GameStateManager.GameState.EVENT);
        Debug.Log("Cargar siguiente nivel");
    }
    
}
