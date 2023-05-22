using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelFinish : MonoBehaviour
{
    public Button restartButton;
    public TMPro.TextMeshProUGUI whoWon;
    public TMPro.TextMeshProUGUI victoryTime;

    /*private void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        if (state == GameState.Victory)
        {
            
        }
    }
    
    private void OnDestroy() => GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;*/

    void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.UpdateGameState(GameState.Victory);
        restartButton.gameObject.SetActive(true);
        victoryTime.gameObject.SetActive(true);
        if (other.CompareTag("Player"))
        {
            whoWon.text = "Player 1 WON";
            
            
        }
        else if (other.CompareTag("Player2"))
        {
            whoWon.text = "Player 2 WON";

        }
    }
}
