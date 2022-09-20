using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Clase que se encarga de mostrar el menú de pausa
public class PauseMenuCanvasManager : MonoBehaviour
{
    [Header("[References]")]
    public GameObject pauseMenuPanel;

    [Header("[Configuration]")]
    public KeyCode pauseButton;

    [Header("[Values]")]
    public bool onPause;


    private void Update()
    {
        if (Input.GetKeyDown(pauseButton) && onPause == false)
            Show_PauseMenu();

        else if (Input.GetKeyDown(pauseButton) && onPause)
            OnClick_Resume();
    }


    private void Show_PauseMenu()
    {
        GameStateManager.instance.SetGameState(GameStateManager.GameState.PAUSE);
        pauseMenuPanel.SetActive(true);
        onPause = true;
    }

    public void OnClick_Resume()
    {
        //(TODO) Al salir de la pausa no debe pasar a Gameplay, si no al último estado en el que estaba
        GameStateManager.instance.SetGameState(GameStateManager.GameState.GAMEPLAY);
        pauseMenuPanel.SetActive(false);
        onPause = false;
    }

    public void OnClick_Exit()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
