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

    [Header("Sounds")] 
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip positiveMenuSound;
    [SerializeField] private AudioClip negativeMenuSound;

    private void Awake()
    {
        Time.timeScale = 1;

        SaveManager.LoadData();
    }

    private void Start()
    {
        TransitionCanvas.instance.Play_ScreenTransition_Out();
    }

    public void PlayLevel(string levelScene)
    {
        StartCoroutine(Coroutine_PlayLevel());

        IEnumerator Coroutine_PlayLevel()
        {
            TransitionCanvas.instance.Play_ScreenTransition_In();
            yield return new WaitForSeconds(1);

            SceneManager.LoadScene(levelScene);
        }
    }

    public void OnClick_Play(string levelName)
    {
        _audioSource.PlayOneShot(positiveMenuSound);
        PlayLevel(levelName);
        GameData._instance.hasPlayed = true;
        
        SaveManager.SaveData();
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




}
