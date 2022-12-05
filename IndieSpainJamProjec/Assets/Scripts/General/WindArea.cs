using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase que se encarga de configurar la dirección y fuerza del viento
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

    //Establece la configuración del viento según los parámetros del editor
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
                areaEffector.forceAngle = 360;
                break;
            case WindDirection.LEFT:
                areaEffector.forceAngle = 180;
                break;
        }
    }
}
