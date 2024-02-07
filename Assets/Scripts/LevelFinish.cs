using System;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LevelFinish : MonoBehaviour
{
    public TMPro.TextMeshProUGUI whoWon;
    public GameObject timeBox;
    public GameObject winningScreen;
    public GameObject UIManager;
    public float delayTime = 1.5f;
    public Button restart;
    public Button menu;
    [SerializeField] private string fileName = "data.csv";

    private void Start()
    {
        restart.gameObject.SetActive(false);
        menu.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            whoWon.text = "<color=#59A973>Player 1 WON";
        }
        else if (other.CompareTag("Player2"))
        {
            whoWon.text = "<color=#2282C0>Player 2 WON";
        }
        GameManager.instance.UpdateGameState(GameState.Victory);
        whoWon.gameObject.SetActive(true);
        timeBox.SetActive(false);
        winningScreen.gameObject.SetActive(true);
        Invoke(nameof(ShowUI), delayTime);
        
        WriteToCSV(Math.Round(TimeManager.instance.victoryTime, 2).ToString("F2"));
        
    }

    private void ShowUI()
    {
        UIManager.SetActive(true);
        restart.gameObject.SetActive(true);
        menu.gameObject.SetActive(true);
    }
    
    private void WriteToCSV(string data)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(filePath))
        {
            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.WriteLine("Time");
            }
        }
        else
        {
            CreateFile(filePath);
        }
        
        using (StreamWriter sw = File.AppendText(filePath))
        {
            sw.WriteLine(data);
        }

    }
    
    private void CreateFile(string filePath)
    {
        using (StreamWriter sw = File.CreateText(filePath))
        {
            sw.WriteLine("Time");
        }
    }
}
