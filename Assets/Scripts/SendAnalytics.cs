using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class SendAnalytics : MonoBehaviour
{
    private bool _consent = true;
    public MovementTime mvTime;
    
    [SerializeField] private string filePath = "Assets/data.csv"; 

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
            WriteToCSV(TimeManager.instance.victoryTime.ToString());
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
    
    private void WriteToCSV(string data)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine("Time");
                }
            }
            
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(data);
            }

            Debug.Log("Data written to CSV file!");
        }
        catch (Exception e)
        {
            Debug.LogError("Error writing to CSV file: " + e.Message);
        }
    }
    
    private void LoadNextScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
}