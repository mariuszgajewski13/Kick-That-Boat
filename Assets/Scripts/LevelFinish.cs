using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelFinish : MonoBehaviour
{
    public TMPro.TextMeshProUGUI whoWon;
    public GameObject timeBox;
    public GameObject winningScreen;
    public GameObject UIManager;
    public float delayTime = 1.5f;
    public Button restart;
    public Button menu;

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
    }

    private void ShowUI()
    {
        UIManager.SetActive(true);
        restart.gameObject.SetActive(true);
        menu.gameObject.SetActive(true);
    }
}
