using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public float time;
    private float victoryTime;
    public TMPro.TextMeshProUGUI timer;
    public TMPro.TextMeshProUGUI finalTimeText;
    private void Awake()
    {
        instance = this;
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }
    
    private void OnDestroy() => GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;

    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        GameState currentState = GameManager.instance.state;
        
        switch (currentState)
        {
            case GameState.Countdown:
                time = 0;
                break;
            case GameState.Race:
                //while (currentState == GameState.Race)
                //{
                    time += Time.deltaTime;
                    timer.text = time.ToString("N3");
                    
                //}
                break;
            case GameState.Victory:
                victoryTime = time;
                finalTimeText.text = victoryTime.ToString("N3");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
        
    }
}    
    
