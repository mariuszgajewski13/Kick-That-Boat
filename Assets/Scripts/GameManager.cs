using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    public static event Action<GameState> OnGameStateChanged; 
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.Countdown);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.Countdown:
                HandleCountdown();
                break;
            case GameState.Race:
                break;
            case GameState.Victory:
                HandleVictory();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    void HandleCountdown()
    {
        
    }

    void HandleVictory()
    {
        
    }
}

public enum GameState
{
    Countdown,
    Race,
    Victory
}