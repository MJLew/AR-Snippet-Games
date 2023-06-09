using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public enum GameState
    {
        notPlaying = 0,
        playing = 1,
    };
    public static GameState gameState;

    void Awake()
    {
        gameState = GameState.notPlaying;
    }

    void Update()
    {
        
    }

    public static void SetGameState(GameState state) 
    {
        gameState = state;
    }
}
