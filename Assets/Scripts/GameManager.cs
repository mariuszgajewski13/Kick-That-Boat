using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState state;
    public static event Action<GameState> OnGameStateChanged;

    public bool race;
    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.Countdown);
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.Countdown:
                HandleCountdown();
                break;
            case GameState.Race:
                HandleRace();
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
        race = false;
    }

    void HandleRace()
    {
        race = true;
    }
}

public enum GameState
{
    Countdown,
    Race,
    Victory
}