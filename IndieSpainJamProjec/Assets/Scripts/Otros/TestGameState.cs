using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameState : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        GameStateManager.instance.currentGameState = GameStateManager.GameState.GAMEPLAY;

    }
    
}
