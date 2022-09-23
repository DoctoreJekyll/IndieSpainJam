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


    private void Awake()
    {
        CreateSingleton();
    }
    private void CreateSingleton()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }


    private void Start()
    {
        initialDoor = GameObject.FindGameObjectWithTag("Initial Door");
        targetDoor = GameObject.FindGameObjectWithTag("Target Door");   
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
        keyMask.transform.position = Camera.main.WorldToScreenPoint(targetDoor.transform.position);
        levelTransition_Animator.Play("TRANSITION IN");
    }

    public void Play_LevelTransition_Out()
    {
        keyTransition.SetActive(true);
        keyMask.transform.position = Camera.main.WorldToScreenPoint(initialDoor.transform.position);
        levelTransition_Animator.Play("TRANSITION OUT");
    }

}
