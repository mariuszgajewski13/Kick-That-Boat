using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;
using UnityEngine.SceneManagement;
using System;

public class SendAnalytics : MonoBehaviour
{
    async void Start()
    {
        await UnityServices.InitializeAsync();
    }
    
    public void ConscentNotGiven()
    {
        LoadNextScene();
    }
	
    public void ConsentGiven()
    {
        AnalyticsService.Instance.StartDataCollection();
        Dictionary<string, object> data = new Dictionary<string, object>(){
            { "winningTime" , TimeManager.instance.victoryTime},
        };
        

        //if(GameManager.instance.state == GameState.Victory)
        if(!GameManager.instance.race)
            AnalyticsService.Instance.CustomData("winningTime", data);
        
        
        
        LoadNextScene();
    }
    
    public void LoadNextScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
}