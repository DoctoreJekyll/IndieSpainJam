using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshotter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.K))
        {
            string nombre = "Test " + System.DateTime.Now.ToString("dd.MM.yyyy-HH.mm") + ".png";
            Debug.Log(nombre);
            ScreenCapture.CaptureScreenshot(nombre, 2);
        }
    }
}
