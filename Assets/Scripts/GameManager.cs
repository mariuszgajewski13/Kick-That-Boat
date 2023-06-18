using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState state;
    public GameObject startScreen;
    public GameObject countdown;
    public static event Action<GameState> OnGameStateChanged;

    public bool race;
    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.Tutorial);
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.Tutorial:
                HandleTutorial();
                break;
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

    void HandleTutorial()
    {
        
    }

    void HandleCountdown()
    {
        startScreen.SetActive(false);
        countdown.SetActive(true);
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
    Tutorial,
    Countdown,
    Race,
    Victory
}