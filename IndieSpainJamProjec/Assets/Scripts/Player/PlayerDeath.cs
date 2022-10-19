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


    //Detiene al jugador, reproduce la animaci?n de muerte y le dice al GameController que ha muerto
    public void OnDeath()
    {
        if(dead == false)
        {
            dead = true;
            GameStateManager.instance.SetGameState(GameStateManager.GameState.EVENT);
            CameraShake.instance.ShakeCamera(CameraShake.ShakeMagnitude.BIG);
            //CinemachineNoise.instance.ShakeCamera(2f,0.5f);
            //playerAnimator.Play("DEATH");

            StartCoroutine(Coroutine_OnDeath());

            IEnumerator Coroutine_OnDeath()
            {
                yield return new WaitForSeconds(0.5f);
                TransitionCanvas.instance.Play_ScreenTransition_In();

                yield return new WaitForSeconds(1);
                LevelManager.instance.PlayerDeath();
            }
        }
    }

}
