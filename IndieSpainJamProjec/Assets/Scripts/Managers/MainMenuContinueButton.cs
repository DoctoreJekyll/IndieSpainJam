using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuContinueButton : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;

    //TODO No se si acabaré manteniendo esta func
    private void Start()
    {
        if (GameData._instance.hasPlayed)
        {
            continueButton.SetActive(true);
        }
    }
}
