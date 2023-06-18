using UnityEngine;
using UnityEngine.UI;

public class LevelFinish : MonoBehaviour
{
    public TMPro.TextMeshProUGUI whoWon;
    public GameObject timeBox;
    public GameObject winningScreen;
    public GameObject UIManager;

    void OnTriggerEnter(Collider other)
    {
        GameManager.instance.UpdateGameState(GameState.Victory);
        winningScreen.gameObject.SetActive(true);
        whoWon.gameObject.SetActive(true);
        timeBox.SetActive(false);
        UIManager.SetActive(true);
        if (other.CompareTag("Player"))
        {
            whoWon.text = "Player 1 WON";
            whoWon.color = new Color(186, 255, 178);
        }
        else if (other.CompareTag("Player2"))
        {
            whoWon.text = "Player 2 WON";
            whoWon.color = new Color(76, 109, 152);
        }
    }
}
