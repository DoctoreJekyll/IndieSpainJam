using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Clase que se encarga de las acciones que toma el jugador en el menú principal
public class MainMenuCanvasManager : MonoBehaviour
{
    [Header("[References]")]
    public GameObject levelsPanel;
    public GameObject creditsPanel;

    [Header("[Values]")]
    public bool levelsPanelOpen;
    public bool creditsPanelOpen;

    [Header("Buttons")] 
    [SerializeField] private Button playButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private bool continueButtonIsOn;

    [Header("Sounds")] 
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip positiveMenuSound;
    [SerializeField] private AudioClip negativeMenuSound;
    
    

    private void Awake()
    {
        Time.timeScale = 1;
    }

    private void Start()
    {
        TransitionCanvas.instance.Play_ScreenTransition_Out();

        if (!DataPersistanceManager.Instance.HasGameData())
        {
            continueButton.interactable = false;
        }
    }

    public void PlayLevel(string levelScene)
    {
        StartCoroutine(Coroutine_PlayLevel());

        IEnumerator Coroutine_PlayLevel()
        {
            TransitionCanvas.instance.Play_ScreenTransition_In();
            yield return new WaitForSeconds(1);

            SceneManager.LoadSceneAsync(levelScene);
        }
    }

    public void OnClick_Play(string levelName)
    {
        DissableForSecurityButtons();
        _audioSource.PlayOneShot(positiveMenuSound);
        DataPersistanceManager.Instance.NewGame();
        PlayLevel(levelName);
    }

    public void OnClick_Continue(string levelName)
    {
        DisableCotinueButton();
        _audioSource.PlayOneShot(positiveMenuSound);
        PlayLevel(levelName);
    }

    public void OnClick_Levels()
    {
        _audioSource.PlayOneShot(positiveMenuSound);
        
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
        _audioSource.PlayOneShot(positiveMenuSound);
        
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
        //Podemos hacer una corutina para que espere a que termine el sonido como en el pausemanager
        _audioSource.PlayOneShot(negativeMenuSound);
        Application.Quit();
    }

    private void DissableForSecurityButtons()
    {
        playButton.interactable = false;
        continueButton.interactable = false;
    }
    
    //TODO - Si no tenemos archivos de guardado habrá que desactivar este boton la primera vez.
    public void DisableCotinueButton()
    {
        continueButton.enabled = continueButtonIsOn;
    }


}
