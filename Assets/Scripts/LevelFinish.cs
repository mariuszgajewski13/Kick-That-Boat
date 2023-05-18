using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelFinish : MonoBehaviour
{
    public RaceTime time;
    public Button restartButton;
    public TMPro.TextMeshProUGUI whoWon;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player 1 WON");
            whoWon.text = "Player 1 WON";
            //disable input
            time.levelFinished = true;
            restartButton.gameObject.SetActive(true);
            
        }
        else if (other.tag == "Player2")
        {
            Debug.Log("Player 2 WON");
            whoWon.text = "Player 2 WON";
            //disable input
            time.levelFinished = true;
            restartButton.gameObject.SetActive(true);
            
        }
    }
}
