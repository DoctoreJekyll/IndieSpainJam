using UnityEngine;

public class PlayerStatesManager : MonoBehaviour
{
    
    public enum  PlayerStates
    {
        WATER,
        ICE,
        GAS
    }

    public PlayerStates playerState = PlayerStates.WATER;
    
}
