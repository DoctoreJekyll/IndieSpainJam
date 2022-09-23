using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempChanger : MonoBehaviour
{
    public int temperatura;
    public int intensidad;
    TempManager tempManager;

    [Header("Sounds")] 
    [SerializeField] private AudioClip _audioClipHot;
    [SerializeField] private AudioClip _audioClipFrost;
    private AudioSource _audioSource;
    private PlayerStatesManager _playerStatesManager;

    public bool playerIsOn;


    // Start is called before the first frame update
    void Start()
    {
        tempManager = GameObject.FindGameObjectWithTag("Temp Manager").GetComponent<TempManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerIsOn = true;
            float potencia = 1 / Vector3.Distance(this.transform.position, collision.transform.position);
            tempManager.ModifyTemperature(temperatura, (intensidad + (potencia * 0.5f)));

        }
    }
    
    
    
}
