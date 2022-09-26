using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrincipalAudioController : MonoBehaviour
{

    public static PrincipalAudioController instance;

    public AudioSource audioS;


    private void Awake()
    {
        CreateSingleton();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            audioS.Stop();
        }
    }

    private void CreateSingleton()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
        
        DontDestroyOnLoad(this.gameObject);
    }
    
    
}
