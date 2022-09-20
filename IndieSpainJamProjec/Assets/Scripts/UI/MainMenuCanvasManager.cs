using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Clase que se encarga de las acciones que toma el jugador en el menú principal
public class MainMenuCanvasManager : MonoBehaviour
{
    
    public void OnClick_Play()
    {
        SceneManager.LoadScene("Level_01");
    }

    public void OnClick_Levels()
    {
        Debug.Log("Muestra el panel de niveles");
    }

    public void OnClick_Exit()
    {
        Application.Quit();
    }

}
