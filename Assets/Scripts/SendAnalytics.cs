using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;

public class SendAnalytics : MonoBehaviour
{
    async void Start()
    {
        await UnityServices.InitializeAsync();
		
        AskForConsent();
    }
	
    void AskForConsent()
    {
        // ... show the player a UI element that asks for consent.
        ConsentGiven();
    }
	
    void ConsentGiven()
    {
        AnalyticsService.Instance.StartDataCollection();
        Dictionary<string, object> data = new Dictionary<string, object>(){
            { "winningTime" , TimeManager.instance.victoryTime},
        };
        

        if(GameManager.instance.state == GameState.Victory)
            AnalyticsService.Instance.CustomData("winningTime", data);
    }
}