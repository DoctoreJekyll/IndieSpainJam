using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuContinueButton : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;

    private void Start()
    {
        if (GameData._instance.hasPlayed)
        {
            continueButton.SetActive(true);
        }
    }
}
