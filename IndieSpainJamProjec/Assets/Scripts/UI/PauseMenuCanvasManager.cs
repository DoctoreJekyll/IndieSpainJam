using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Clase que se encarga de mostrar el men� de pausa
public class PauseMenuCanvasManager : MonoBehaviour
{
    [Header("[References]")]
    public GameObject pauseMenuPanel;

    [Header("[Configuration]")]
    public KeyCode pauseButton;

    [Header("[Values]")]
    public bool onPause;

    [Header("Sounds")] 
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip positiveSound;
    [SerializeField] private AudioClip negativeSound;
    

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
        //(TODO) Al salir de la pausa no debe pasar a Gameplay, si no al �ltimo estado en el que estaba
        _audioSource.PlayOneShot(positiveSound);
        GameStateManager.instance.SetGameState(GameStateManager.GameState.GAMEPLAY);
        pauseMenuPanel.SetActive(false);
        onPause = false;
    }

    public void OnClick_Exit()
    {
        //Esto probablemente no suene ahora mismo porque carga del tiron
        _audioSource.PlayOneShot(negativeSound);
        SceneManager.LoadSceneAsync("Main Menu");
        
        //Propuesta
        //StartCoroutine(LoadSceneAfterSound());

    }
    
    //Propuesta
    private IEnumerator LoadSceneAfterSound()
    {
        _audioSource.PlayOneShot(negativeSound);
        //Esto espera a que acabe el sonido y despues carga
        yield return new WaitForSeconds(negativeSound.length);
        SceneManager.LoadScene("Main Menu");
    }

}
