using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Clase que se encarga de actualizar la UI de la temperatura
public class UI_TemperatureCanvas : MonoBehaviour
{
    [Header("[References]")]
    public Image tempFiller;
    public TextMeshProUGUI degreesText;
    public Animator thermometerAnimator;

    [Header("[Values]")]
    public float currentTemp;


    //Actualizamos la UI con la temperatura actual
    public void Refresh_TemperatureCanvas(float newCurrentTemp)
    {
        currentTemp = newCurrentTemp;
        degreesText.text = currentTemp.ToString("F0") + "º";

        //Subimos o bajamos el "mercurio" del termómetro en la UI según la temperatura actual
        tempFiller.rectTransform.localScale = new Vector3(tempFiller.rectTransform.localScale.x, currentTemp / 100, tempFiller.rectTransform.localScale.z);

        Check_TermAnimation();
    }


    //Comprueba la temperatura actual para reproducir o no animaciones en el termómetro
    private void Check_TermAnimation()
    {
        //Si la temperatura es normal, detenemos las animaciones
        if (currentTemp > 25 && currentTemp < 75)
        {
            thermometerAnimator.SetBool("IDLE", true);
            thermometerAnimator.SetBool("SHAKE", false);
            thermometerAnimator.SetBool("POP", false);
        }

        //Si la temperatura está cerca de que cambie el estado del jugador, hace la animación de shake
        if ((currentTemp < 20 && currentTemp > 1) || (currentTemp > 80 && currentTemp <= 99))
        {
            thermometerAnimator.SetBool("IDLE", false);
            thermometerAnimator.SetBool("SHAKE", true);
            thermometerAnimator.SetBool("POP", false);
        }

        //Si la temperatura ha llegado a un extremo, hace la animación de pop
        if (currentTemp >= 100 || currentTemp <= 0)
        {
            thermometerAnimator.SetBool("IDLE", false);
            thermometerAnimator.SetBool("SHAKE", false);
            thermometerAnimator.SetBool("POP", true);
        }
    }
}
