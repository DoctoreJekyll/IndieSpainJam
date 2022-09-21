using System;
using UnityEngine;

public class PlayerStatesManager : MonoBehaviour
{

    [Header("Players")]
    [SerializeField] private GameObject waterPlayer;
    [SerializeField] private GameObject icePlayer;
    [SerializeField] private GameObject gasPlayer;

    [Header("Temperature")] 
    [SerializeField] private float maxTemperature;
    [SerializeField] private float minTemperature;
    
    [Header("Controller")]
    [SerializeField] private TempManager _tempManager;


    [Header("Test")]
    [SerializeField] private Transform lakituTransform;
    private Vector2 playerPos;

    
    private void Awake()
    {
        _tempManager = FindObjectOfType<TempManager>();
    }

    public enum  PlayerStates
    {
        WATER,
        ICE,
        GAS
    }

    public PlayerStates playerState = PlayerStates.WATER;

    private void Update()
    {
        ChangePlayersWithTemp();
        SwitchBetweenPlayers();
        
        
        
    }

    private void ChangePlayersWithTemp()
    {
        if (_tempManager.temperatura <= 0)
        {
            Debug.Log("hielo noeke");
            playerState = PlayerStates.ICE;
        }

        if (_tempManager.temperatura >= 100)
        {
            playerState = PlayerStates.GAS;
        }

        if (playerState == PlayerStates.ICE && _tempManager.temperatura >= minTemperature)
        {
            playerState = PlayerStates.WATER;
        }

        if (playerState == PlayerStates.GAS && _tempManager.temperatura <= maxTemperature)
        {
            playerState = PlayerStates.WATER;
        }


    }

    private void SwitchBetweenPlayers()
    {
        switch (playerState)
        {
            case PlayerStates.WATER:
                //DesactivateAllObj();
                //waterPlayer.gameObject.transform.position = lakituTransform.position;
                waterPlayer.gameObject.SetActive(true);
                icePlayer.gameObject.SetActive(false);
                gasPlayer.gameObject.SetActive(false);
                break;
                
            case  PlayerStates.ICE:
                //DesactivateAllObj();
                icePlayer.gameObject.transform.position = lakituTransform.position;
                icePlayer.gameObject.SetActive(true);
                waterPlayer.gameObject.SetActive(false);
                gasPlayer.gameObject.SetActive(false);
                break;
            
            case  PlayerStates.GAS:
                //DesactivateAllObj();
                gasPlayer.gameObject.transform.position = lakituTransform.position;
                gasPlayer.gameObject.SetActive(true);
                icePlayer.gameObject.SetActive(false);
                waterPlayer.gameObject.SetActive(false);
                break;
            
            default:
                break;

        }
    }

    private void DesactivateAllObj()
    {
        

    }
    
}
