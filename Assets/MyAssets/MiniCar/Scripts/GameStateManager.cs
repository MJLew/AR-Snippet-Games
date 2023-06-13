using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public MiniCarUI miniCarUI; // Set in inspector
    public static int packagesCollected;
    public enum GameState
    {
        notPlaying = 0,
        playing = 1,
    };
    public static GameState gameState;

    void Awake()
    {
        gameState = GameState.notPlaying;
        packagesCollected = 0;
    }

    void Update()
    {
        
    }

    public void SetGameState(GameState state) 
    {
        gameState = state;
    }

    public void SetPackagesCollected(int count) 
    { 
        packagesCollected = count;
        miniCarUI.setPackageCount(packagesCollected);
    }
}
