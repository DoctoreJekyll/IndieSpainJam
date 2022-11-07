using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempChangerZone : MonoBehaviour
{

    private PlayerStatesManager playerState;
    private TempManager tempManager;
    public enum StateToChange { ICE, WATER, GAS }
    
    [Header("[Configuration]")]
    [SerializeField] private StateToChange newState;


    private void Awake()
    {
        playerState = FindObjectOfType<PlayerStatesManager>();
        tempManager = FindObjectOfType<TempManager>();
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            SwapPlayerState();
        }
    }


    private void SwapPlayerState()
    {
        switch (newState)
        {
            case StateToChange.ICE:
                if(playerState.currentPlayerState == PlayerStatesManager.PlayerState.LIQUID)
                {
                    playerState.SwitchBetweenPlayers(PlayerStatesManager.PlayerState.SOLID);
                    tempManager.SetTemperature(0);
                }

                else if(playerState.currentPlayerState == PlayerStatesManager.PlayerState.GAS)
                {
                    playerState.SwitchBetweenPlayers(PlayerStatesManager.PlayerState.LIQUID);
                    tempManager.SetTemperature(75);
                }
                break;


            case StateToChange.WATER:
                if (playerState.currentPlayerState == PlayerStatesManager.PlayerState.SOLID)
                {
                    playerState.SwitchBetweenPlayers(PlayerStatesManager.PlayerState.LIQUID);
                    tempManager.SetTemperature(25);
                }

                else if(playerState.currentPlayerState == PlayerStatesManager.PlayerState.GAS)
                {
                    playerState.SwitchBetweenPlayers(PlayerStatesManager.PlayerState.LIQUID);
                    tempManager.SetTemperature(75);
                }
                break;


            case StateToChange.GAS:
                if (playerState.currentPlayerState == PlayerStatesManager.PlayerState.SOLID)
                {
                    playerState.SwitchBetweenPlayers(PlayerStatesManager.PlayerState.LIQUID);
                    tempManager.SetTemperature(25);
                }

                else if (playerState.currentPlayerState == PlayerStatesManager.PlayerState.LIQUID)
                {
                    playerState.SwitchBetweenPlayers(PlayerStatesManager.PlayerState.GAS);
                    tempManager.SetTemperature(100);
                }
                break;
        }
    }
    

}
