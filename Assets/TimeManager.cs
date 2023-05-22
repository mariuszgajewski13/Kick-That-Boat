using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public TimeManager instance;
    private float time = 0f;
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
        if (state == GameState.Race)
        {
            time += Time.deltaTime;
            timer.text = time.ToString("N3");
        }

        if (state == GameState.Victory)
        {
            victoryTime = time;
            finalTimeText.text = victoryTime.ToString("N3");
        }
    }
}    
    
