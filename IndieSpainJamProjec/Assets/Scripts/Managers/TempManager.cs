using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TempManager : MonoBehaviour
{
    public float temperatura;
    public TextMeshProUGUI temptext;
    public Slider termometro;
    // Start is called before the first frame update
    void Start()
    {
        //test
        temperatura = 30f;
        temptext.text = temperatura.ToString("F2");
        termometro.value = (int)temperatura;
    }

    // Update is called once per frame
    void Update()
    {
        if(temperatura <= 0f)
        {
            temperatura = 0.01f;
        }
        else if(temperatura >= 101f)
        {
            temperatura = 100.00f;
        }
        if (Input.GetKey(KeyCode.O))
        {
            cambiaTemperatura(-5f, 5f);
        }
        else if (Input.GetKey(KeyCode.P))
        {
            cambiaTemperatura(5f, 5f);
        }
    }
    public void cambiaTemperatura(float modificador,float intensidad)
    {
        if(temperatura > 0f && temperatura < 101f)
        {
            temperatura = temperatura + ((modificador * intensidad) * Time.deltaTime);
        }
        temptext.text = temperatura.ToString("F2");
        termometro.value = (int)temperatura;
    }
}
