using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase que se encarga de controlar la muerte del jugador
public class PlayerDeath : MonoBehaviour
{
    [Header("[References]")]
    public Animator playerAnimator;

    [Header("[Configuration]")]
    public float timeUntilSceneReset = 1.5f;


    //Detiene al jugador, reproduce la animaci�n de muerte y le dice al GameController que ha muerto
    public void OnDeath()
    {
        GameStateManager.instance.SetGameState(GameStateManager.GameState.EVENT);
        CameraShake.instance.ShakeCamera(CameraShake.ShakeMagnitude.BIG);
        //playerAnimator.Play("DEATH");

        StartCoroutine(Coroutine_OnDeath());

        IEnumerator Coroutine_OnDeath()
        {
            yield return new WaitForSeconds(timeUntilSceneReset);
            LevelManager.instance.PlayerDeath();
        }
    }

}
