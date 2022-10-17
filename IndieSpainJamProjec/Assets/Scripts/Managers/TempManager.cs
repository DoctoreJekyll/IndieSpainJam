using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//Clase que se encarga de modificar la temperatura del jugador y la ambiental en el nivel
public class TempManager : MonoBehaviour
{
    [Header("[References]")]
    public UI_TemperatureCanvas tempCanvas;

    [Header("[Configuration]")]
    public float initialTemp;
    public float ambientTemp;
    public float ambientTemp_Intensity;
    public bool forceAmbientTemperature;

    [Header("[Values]")]
    public float currentTemp;
    

    //Establecemos la temperatura inicial
    void Start()
    {
        tempCanvas = GameObject.FindGameObjectWithTag("Temp Canvas").GetComponent<UI_TemperatureCanvas>();

        currentTemp = initialTemp;
        tempCanvas.Refresh_TemperatureCanvas(currentTemp);
    }


    //Si esta activado la temperatura ambiente, la modificamos constantemente
    private void Update()
    {
        if (forceAmbientTemperature)
            ModifyAmbientTemperature();
    }


    //Modifica la temperatura actual, actualiza la UI y comprueba si el termómetro tiene que reproducir animaciones
    public void ModifyTemperature(float modificador,float intensidad)
    {
        if(currentTemp >= 0f && currentTemp <= 100f)
            currentTemp = currentTemp + ((modificador * intensidad) * Time.deltaTime);

        if (currentTemp > 100f)
            currentTemp = 100f;

        if (currentTemp < 0f)
            currentTemp = 0f;

        tempCanvas.Refresh_TemperatureCanvas(currentTemp);
    }


    //Modifica la temperatura de forma constante hasta llegar a la temperatura ambiente
    private void ModifyAmbientTemperature()
    {
        if(currentTemp > ambientTemp)
            ModifyTemperature(-1, ambientTemp_Intensity);

        if (currentTemp < ambientTemp)
            ModifyTemperature(1, ambientTemp_Intensity);
    }

}
