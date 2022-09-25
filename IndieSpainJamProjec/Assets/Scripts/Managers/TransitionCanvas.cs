using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionCanvas : MonoBehaviour
{
    public static TransitionCanvas instance;

    [Header("[References]")]
    public Animator screenTransition_Animator;
    public Animator levelTransition_Animator;
    public Image keyMask;
    public GameObject screenTransition, keyTransition;
    private GameObject initialDoor, targetDoor;

    private Vector3 initialDoorPos, targetDoorPos;

    public Camera auxCamera;
    public bool itsMainMenu;

    private void Awake()
    {
        if(itsMainMenu == false)
        {
            initialDoor = GameObject.FindGameObjectWithTag("Initial Door");
            targetDoor = GameObject.FindGameObjectWithTag("Target Door");

            auxCamera = GameObject.FindGameObjectWithTag("Aux Camera").GetComponent<Camera>();

            initialDoorPos = Camera.main.WorldToScreenPoint(initialDoor.transform.position);
            targetDoorPos = auxCamera.WorldToScreenPoint(targetDoor.transform.position);
        }

        CreateSingleton();
    }

    private void Start()
    {
        if(itsMainMenu == false)
            StartCoroutine(PatchTransition());
    }

    private void CreateSingleton()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }   



    public void Play_ScreenTransition_In()
    {
        screenTransition.SetActive(true);
        screenTransition_Animator.Play("TRANSITION IN");
    }

    public void Play_ScreenTransition_Out()
    {
        screenTransition.SetActive(true);
        screenTransition_Animator.Play("TRANSITION OUT");
    }

    public void Play_LevelTransition_In()
    {
        keyMask.transform.position = targetDoorPos;
        levelTransition_Animator.Play("TRANSITION IN");
    }

    public void Play_LevelTransition_Out()
    {
        keyTransition.SetActive(true);
        keyMask.transform.position = initialDoorPos;
        levelTransition_Animator.Play("TRANSITION OUT");
    }

    private IEnumerator PatchTransition()
    {
        yield return new WaitForEndOfFrame();
        auxCamera.enabled = false;
    }

}
