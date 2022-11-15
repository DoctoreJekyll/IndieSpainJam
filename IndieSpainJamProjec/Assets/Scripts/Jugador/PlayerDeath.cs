using System.Collections;
using UnityEngine;

//Clase que se encarga de controlar la muerte del jugador
public class PlayerDeath : MonoBehaviour
{
    [Header("[References]")] 
    private Rigidbody2D rb2d;
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
        rb2d = GetComponent<Rigidbody2D>();
    }

    //Posiciono al jugador en los valores recogidos por el trigger del checkpoint y retorno el estado y el bool
    private void ReturnToLastCheckPoint()
    {
        playerCheckPointValues = GetComponentInParent<PlayerCheckPointValues>();
        
        transform.position = playerCheckPointValues.playerCheckPoinPositionValue;
        tempManager.SetTemperature(playerCheckPointValues.checkPointTemperatureValue);
        
        ReturnPlayerValues();
    }

    //Devolvemos el estado de juego
    private void ReturnPlayerValues()
    {
        if (GameStateManager.instance.currentGameState != GameStateManager.GameState.GAMEPLAY)
        {
            GameStateManager.instance.SetGameState(GameStateManager.GameState.GAMEPLAY);
        }

        rb2d.bodyType = RigidbodyType2D.Dynamic;
        dead = false;
    }
    

    //Detiene al jugador, reproduce la animaci?n de muerte y le dice al GameController que ha muerto
    public void OnDeath()
    {
        if(dead == false)
        {
            WhenDeadStuffs();
            StartCoroutine(Coroutine_OnDeath());

            IEnumerator Coroutine_OnDeath()
            {
                yield return new WaitForSeconds(0.25f);
                TransitionCanvas.instance.Play_ScreenTransition_In();

                yield return new WaitForSeconds(1.5f);
                ReturnToLastCheckPoint();
                TransitionCanvas.instance.Play_ScreenTransition_Out();
            }
        }
    }

    //Cosas que pasan cuando mueres noeke
    private void WhenDeadStuffs()
    {
        dead = true;
        CinemachineNoise.instance.ShakeCamera(2f,0.5f);
        GameStateManager.instance.SetGameState(GameStateManager.GameState.EVENT);
        rb2d.velocity = Vector2.zero;
        rb2d.bodyType = RigidbodyType2D.Kinematic;
        
        //CameraShake.instance.ShakeCamera(CameraShake.ShakeMagnitude.BIG);
        //playerAnimator.Play("DEATH");
    }

    private void DeadWithoutCheckPoint()
    {
        Vector3 position = GameObject.FindGameObjectWithTag("Initial Door").transform.position;
        transform.position = position;
    }

}
