using UnityEngine;
using UnityEngine.InputSystem;

//Clase que se encarga de cambiar el estado del jugador
public class PlayerStatesManager : MonoBehaviour
{
    public enum PlayerState { SOLID, LIQUID, GAS }

    [Header("[Players]")]
    [SerializeField] private GameObject waterPlayer;
    [SerializeField] private GameObject icePlayer;
    [SerializeField] private GameObject gasPlayer;

    [Header("[Input Actions]")] 
    [SerializeField] private PlayerInput[] playersInputsComponent;

    [Header("[Temperature]")] 
    [SerializeField] private float maxTemperature;
    [SerializeField] private float minTemperature;
    
    [Header("[Controller]")]
    private TempManager _tempManager;

    [Header("[Values]")]
    public PlayerState currentPlayerState;


    private void Awake()
    {
        _tempManager = FindObjectOfType<TempManager>();
        currentPlayerState = PlayerState.LIQUID;
    }

    private void Update()
    {
        ChangePlayersWithTemp();
    }
    

    //Comprueba la temperatura del jugador y cambia el estado del jugador si es necesario
    private void ChangePlayersWithTemp()
    {
        //Si la temperatura es 0º, se convierte en hielo
        if (_tempManager.currentTemp <= 0)
        {
            currentPlayerState = PlayerState.SOLID;
            SwitchBetweenPlayers(PlayerState.SOLID);
        }

        //Si la temperatura es 100º, se convierte en gas
        if (_tempManager.currentTemp >= 100)
        {
            currentPlayerState = PlayerState.GAS;
            SwitchBetweenPlayers(PlayerState.GAS);
        }

        //Si el jugador es hielo o gas y su temperatura se normaliza, se convierte en agua
        if ((currentPlayerState == PlayerState.SOLID && _tempManager.currentTemp >= minTemperature) || (currentPlayerState == PlayerState.GAS && _tempManager.currentTemp <= maxTemperature))
        {
            currentPlayerState = PlayerState.LIQUID;
            SwitchBetweenPlayers(PlayerState.LIQUID);
        }

    }


    //Cambia el elemental con el que juega el jugador
    public void SwitchBetweenPlayers(PlayerState newPlayerState)
    {
        switch (newPlayerState)
        {
            case PlayerState.SOLID:
                PlayerIceActivateStuffs();
                break;

            case PlayerState.LIQUID:
                PlayerWaterActivateStuffs();
                break;
            
            case PlayerState.GAS:
                PlayerGasActivateStuffs();
                break;
        }
    }

    private void PlayerIceActivateStuffs()
    {
        icePlayer.gameObject.SetActive(true);
        waterPlayer.gameObject.SetActive(false);
        gasPlayer.gameObject.SetActive(false);

        playersInputsComponent[1].enabled = true;
        playersInputsComponent[0].enabled = false;
        playersInputsComponent[2].enabled = false;
    }
    
    private void PlayerWaterActivateStuffs()
    {
        waterPlayer.gameObject.SetActive(true);
        icePlayer.gameObject.SetActive(false);
        gasPlayer.gameObject.SetActive(false);
        
        playersInputsComponent[1].enabled = false;
        playersInputsComponent[0].enabled = true;
        playersInputsComponent[2].enabled = false;
    }
    
    private void PlayerGasActivateStuffs()
    {
        gasPlayer.gameObject.SetActive(true);
        icePlayer.gameObject.SetActive(false);
        waterPlayer.gameObject.SetActive(false);
        
        playersInputsComponent[1].enabled = false;
        playersInputsComponent[0].enabled = false;
        playersInputsComponent[2].enabled = true;
    }
}
