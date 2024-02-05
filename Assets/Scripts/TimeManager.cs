using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public float time;
    public float victoryTime;
    public TMPro.TextMeshProUGUI timer;
    public TMPro.TextMeshProUGUI finalTimeText;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }
    
    private void OnDestroy() => GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;

    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        state = GameManager.instance.state;
        
        switch (state)
        {
            case GameState.Tutorial:
                break;
            case GameState.Countdown:
                time = 0;
                break;
            case GameState.Race:
                break;
            case GameState.Victory:
                //race = false;
                victoryTime = time;
                finalTimeText.text = victoryTime.ToString("N3");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
        
    }

    private void Update()
    {
        if (GameManager.instance.race)
        {
            time += Time.deltaTime;
            timer.text = time.ToString("N3");
            
        }
    }
}    
    
