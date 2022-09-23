using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Clase que se encarga de las acciones que toma el jugador en el menú principal
public class MainMenuCanvasManager : MonoBehaviour
{
    [Header("[References]")]
    public GameObject levelsPanel;
    public GameObject creditsPanel;

    [Header("[Values]")]
    public bool levelsPanelOpen;
    public bool creditsPanelOpen;


    public void OnClick_Play()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void OnClick_Levels()
    {
        if(levelsPanelOpen == false)
        {
            levelsPanelOpen = true;

            if(creditsPanelOpen == true)
            {
                creditsPanelOpen = false;
                creditsPanel.SetActive(false);
            }
        }
        else
            levelsPanelOpen = false;

        levelsPanel.SetActive(levelsPanelOpen);
    }

    public void OnClick_Credits()
    {
        if (creditsPanelOpen == false)
        {
            creditsPanelOpen = true;

            if (levelsPanelOpen == true)
            {
                levelsPanelOpen = false;
                levelsPanel.SetActive(false);
            }
        }
        else
            creditsPanelOpen = false;

        creditsPanel.SetActive(creditsPanelOpen);
    }

    public void OnClick_Exit()
    {
        Application.Quit();
    }

}
