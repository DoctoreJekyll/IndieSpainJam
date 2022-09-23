using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTransitionCanvas : MonoBehaviour
{
    public static LevelTransitionCanvas instance;

    [Header("[References]")]
    public Animator transitionAnimator;
    public Image keyMask;
    public GameObject keyTransition;
    private GameObject initialDoor, targetDoor;


    private void Awake()
    {
        keyTransition.SetActive(true);
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

        Play_Transition_Out();
    }    


    public void Play_Transition_In()
    {
        keyMask.transform.position = Camera.main.WorldToScreenPoint(targetDoor.transform.position);
        transitionAnimator.Play("TRANSITION IN");
    }

    public void Play_Transition_Out()
    {
        keyMask.transform.position = Camera.main.WorldToScreenPoint(initialDoor.transform.position);
        transitionAnimator.Play("TRANSITION OUT");
    }

}
