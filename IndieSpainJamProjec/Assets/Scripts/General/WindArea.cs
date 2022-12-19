using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase que se encarga de configurar la direcci�n y fuerza del viento
public class WindArea : MonoBehaviour
{
    public enum WindDirection { UP, RIGHT, DOWN, LEFT }

    [Header("[References]")]
    public AreaEffector2D areaEffector;

    [Header("[Configuration]")]
    public WindDirection windDirection;
    public float windForce = 35;


    void OnEnable()
    {
        SetWindConfiguration();
    }

    //Establece la configuraci�n del viento seg�n los par�metros del editor
    private void SetWindConfiguration()
    {
        areaEffector.forceMagnitude = windForce;

        switch (windDirection)
        {
            case WindDirection.UP:
                areaEffector.forceAngle = 90;
                break;
            case WindDirection.RIGHT:
                areaEffector.forceAngle = 0;
                break;
            case WindDirection.DOWN:
                areaEffector.forceAngle = 270;
                break;
            case WindDirection.LEFT:
                areaEffector.forceAngle = 180;
                break;
        }
    }

    public float testforce;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            ImproveJump improveJump = col.GetComponent<ImproveJump>();
            if (improveJump != null)
            {
                improveJump.isOnWindArea = true;
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ImproveJump improveJump = other.GetComponent<ImproveJump>();
            if (improveJump != null)
            {
                improveJump.isOnWindArea = false;
            }
        }
    }
    
}
