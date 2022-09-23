using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TempManager : MonoBehaviour
{
    [Header("[References]")]
    public Image tempFiller;
    public TextMeshProUGUI temptext;
    public Animator termAnimator;

    [Header("[Configuration]")]
    public float initialTemp;

    [Header("[Values]")]
    public float currentTemp;
    


    // Start is called before the first frame update
    void Start()
    {
        currentTemp = initialTemp;
        temptext.text = currentTemp.ToString("F0") + "º";
    }


    public void ModifyTemperature(float modificador,float intensidad)
    {
        if(currentTemp >= 0f && currentTemp <= 100f)
        {
            currentTemp = currentTemp + ((modificador * intensidad) * Time.deltaTime);
        }

        if (currentTemp > 100f)
            currentTemp = 100f;

        if (currentTemp < 0f)
            currentTemp = 0f;

        temptext.text = currentTemp.ToString("F0") +"º";
        tempFiller.rectTransform.localScale = new Vector3(tempFiller.rectTransform.localScale.x, currentTemp / 100, tempFiller.rectTransform.localScale.z);

        Debug.Log("Modificando");
        CheckTermAnimation();
    }


    private void CheckTermAnimation()
    {
        //Si la temperatura es normal, detenemos las animaciones
        if (currentTemp > 25 && currentTemp < 75) 
        {
            termAnimator.SetBool("IDLE", true);
            termAnimator.SetBool("SHAKE", false);
            termAnimator.SetBool("POP", false);
        }

        //Si la temperatura está cerca de que cambie el estado del jugador, hace la animación de shake
        if ((currentTemp < 20 && currentTemp > 1) || (currentTemp > 80 && currentTemp <= 99))
        {
            termAnimator.SetBool("IDLE", false);
            termAnimator.SetBool("SHAKE", true);
            termAnimator.SetBool("POP", false);
        }

        //Si la temperatura ha llegado a un extremo, hace la animación de pop
        if(currentTemp >= 100 || currentTemp <= 0)
        {
            termAnimator.SetBool("IDLE", false);
            termAnimator.SetBool("SHAKE", false);
            termAnimator.SetBool("POP", true);
        }
    }

}
