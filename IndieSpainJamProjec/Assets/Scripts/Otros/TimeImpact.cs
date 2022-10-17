using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase que se encarga de ralentizar el tiempo levemente para darle gamefeel
//en momentos importantes de la partida como los impactos
public class TimeImpact : MonoBehaviour
{
    public enum SlowMagnitude { NULL, SMALL, MEDIUM, BIG }
    public static TimeImpact instance;

    [Header("[Configuration]")]
    [SerializeField] private float smallMagnitude = 0.25f;
    [SerializeField] private float normalMagnitude = 0.5f;
    [SerializeField] private float bigMagnitude = 0.75f;

    //Comento el singleton mientras no usemos la clase para no quemar de singleton el proyecto
    private void Awake()
    {
        //CreateSingleton();
    }
    private void CreateSingleton()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }


    public void ImpactTime(SlowMagnitude newSlowMagnitude)
    {
        float slowDuration = 0;
        float slowMagnitude = 0;

        switch (newSlowMagnitude)
        {
            case SlowMagnitude.NULL:
                slowDuration = 0;
                slowMagnitude = 0;
                break;
            case SlowMagnitude.SMALL:
                slowDuration = 0.25f;
                slowMagnitude = smallMagnitude;
                break;
            case SlowMagnitude.MEDIUM:
                slowDuration = 0.5f;
                slowMagnitude = normalMagnitude;
                break;
            case SlowMagnitude.BIG:
                slowDuration = 1;
                slowMagnitude = bigMagnitude;
                break;
        }

        StartCoroutine(Coroutine_SlowDownTime(slowDuration, slowMagnitude));
    }


    IEnumerator Coroutine_SlowDownTime(float slowDuration, float slowMagnitude)
    {
        Time.timeScale -= slowMagnitude;
        float currentTime = 0;

        while (currentTime < slowDuration)
        {
            currentTime += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = 1;
    }
}
