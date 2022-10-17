using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionCanvas : MonoBehaviour
{
    public static TransitionCanvas instance;

    [Header("[References]")]
    [SerializeField] private Animator screenTransition_Animator;
    [SerializeField] private Animator levelTransition_Animator;
    [SerializeField] private Image keyMask;
    [SerializeField] private GameObject screenTransition, keyTransition;
    private GameObject initialDoor, targetDoor;

    private Vector3 initialDoorPos, targetDoorPos;

    [SerializeField] private Camera auxCamera;
    [SerializeField] private bool itsMainMenu;

    private void Awake()
    {
        GetReferences();
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

    private void GetReferences()
    {
        if(itsMainMenu == false)
        {
            initialDoor = GameObject.FindGameObjectWithTag("Initial Door");
            targetDoor = GameObject.FindGameObjectWithTag("Target Door");

            auxCamera = GameObject.FindGameObjectWithTag("Aux Camera").GetComponent<Camera>();

            initialDoorPos = Camera.main.WorldToScreenPoint(initialDoor.transform.position);
            targetDoorPos = auxCamera.WorldToScreenPoint(targetDoor.transform.position);
        }
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
