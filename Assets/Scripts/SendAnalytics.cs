using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;
using UnityEngine.SceneManagement;
using System;

public class SendAnalytics : MonoBehaviour
{
    public bool conscent;
    
    async void Start()
    {
        await UnityServices.InitializeAsync();
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }
    
    private void OnDestroy() => GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;

    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        if (state == GameState.Victory && conscent)
        {
            SendData();
        }
    }
    
    public void ConscentNotGiven()
    {
        LoadNextScene();
    }

    public void ConsentGiven()
    {
        conscent = true;
        LoadNextScene();
    }


    public void SendData()
    {
        AnalyticsService.Instance.StartDataCollection();
        Dictionary<string, object> data = new Dictionary<string, object>(){
            { "winningTime" , TimeManager.instance.victoryTime},
        };

        AnalyticsService.Instance.CustomData("winningTime", data);
        
        LoadNextScene();
    }
    
    public void LoadNextScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
}