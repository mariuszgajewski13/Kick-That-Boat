using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelFinish : MonoBehaviour
{
    public TMPro.TextMeshProUGUI whoWon;
    public GameObject timeBox;
    public GameObject winningScreen;
    public GameObject UIManager;

    void OnTriggerEnter(Collider other)
    {
        GameManager.instance.UpdateGameState(GameState.Victory);
        StartCoroutine(Wait(20));
        whoWon.gameObject.SetActive(true);
        timeBox.SetActive(false);
        winningScreen.gameObject.SetActive(true);
        UIManager.SetActive(true);
        if (other.CompareTag("Player"))
        {
            whoWon.text = "<color=#baffb2>Player 1 WON";
            whoWon.fontMaterial.color = new Color(186, 255, 178);
        }
        else if (other.CompareTag("Player2"))
        {
            whoWon.text = "<color=#4c6d98>Player 2 WON";
            whoWon.color = new Color(76, 109, 152);
        }
    }

    IEnumerator Wait(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
