using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;
using UnityEngine.SceneManagement;


public class SendAnalytics : MonoBehaviour
{
    private bool _consent = true;
    public MovementTime mvTime;

    private async void Start()
    {
        await UnityServices.InitializeAsync();
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }
    
    private void OnDestroy() => GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;

    // ReSharper disable Unity.PerformanceAnalysis
    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        if (state == GameState.Victory && _consent)
        {
            SendData();
            
        }
    }
    
    public void ConsentNotGiven()
    {
        LoadNextScene();
    }

    public void ConsentGiven()
    {
        _consent = true;
        LoadNextScene();
    }


    private void SendData()
    {
        AnalyticsService.Instance.StartDataCollection();
        
        Dictionary<string, object> time = new Dictionary<string, object>(){
            { "winningTime" , TimeManager.instance.victoryTime}
        };
        
        Dictionary<string, object> speed = new Dictionary<string, object>(){
            { "avgSpeed" , mvTime.avgSpeed},
            { "timeBetweenPresses" , mvTime.timeBetweenPresses},
        };

        AnalyticsService.Instance.CustomData("winningTime", time);
        AnalyticsService.Instance.CustomData("AverageSpeed", speed);
        Debug.Log("data send");
        Debug.Log(TimeManager.instance.victoryTime);
        Debug.Log(mvTime.avgSpeed);
        Debug.Log(mvTime.timeBetweenPresses);
    }
    
    private void LoadNextScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
}