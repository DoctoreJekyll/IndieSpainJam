using System;
using UnityEngine;

public class PlayerStatesManager : MonoBehaviour
{

    [SerializeField] private GameObject waterPlayer;
    [SerializeField] private GameObject icePlayer;
    [SerializeField] private GameObject gasPlayer;
    
    public enum  PlayerStates
    {
        WATER,
        ICE,
        GAS
    }

    public PlayerStates playerState = PlayerStates.WATER;

    private void Update()
    {
        ChangePlayersWithKey();
        SwitchBetweenPlayers();
    }

    private void ChangePlayersWithKey()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            playerState = PlayerStates.WATER;
        }
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            playerState = PlayerStates.ICE;
        }
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            playerState = PlayerStates.GAS;
        }
    }

    private void SwitchBetweenPlayers()
    {
        switch (playerState)
        {
            case PlayerStates.WATER:
                DesactivateAllObj();
                waterPlayer.gameObject.SetActive(true);
                break;
                
            case  PlayerStates.ICE:
                DesactivateAllObj();
                icePlayer.gameObject.SetActive(true);
                break;
            
            case  PlayerStates.GAS:
                Debug.Log("Plof");
                break;
            
            default:
                break;

        }
    }

    private void DesactivateAllObj()
    {
        waterPlayer.gameObject.SetActive(false);
        icePlayer.gameObject.SetActive(false);
        //Gas
    }
    
}
