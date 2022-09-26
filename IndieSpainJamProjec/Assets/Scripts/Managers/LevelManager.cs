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

    [Header("[Configuration]")]
    public KeyCode resetKey;
    public string currentLevel;
    public string nextLevelScene;


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
        player = GameObject.FindGameObjectWithTag("Player");
        initialDoor = GameObject.FindGameObjectWithTag("Initial Door");
        PrepareLevel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(resetKey))
        {
            PlayerDeath player = GameObject.FindObjectOfType<PlayerDeath>();
            player.OnDeath();
        }
    }

    public void PrepareLevel()
    {
        player.transform.position = initialDoor.transform.position;
        GameStateManager.instance.SetGameState(GameStateManager.GameState.GAMEPLAY);
        TransitionCanvas.instance.Play_LevelTransition_Out();
    }

    public void PlayerDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelCompleted()
    {
        GameStateManager.instance.SetGameState(GameStateManager.GameState.EVENT);
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Animator>().enabled = false;
        PlayerPrefs.SetInt(currentLevel, 1);

        StartCoroutine(Coroutine_NextLevel());

        IEnumerator Coroutine_NextLevel()
        {
            TransitionCanvas.instance.Play_LevelTransition_In();
            yield return new WaitForSeconds(4);

            SceneManager.LoadScene(nextLevelScene);
        }
    }
    
}
