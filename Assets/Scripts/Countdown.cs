using System.Collections;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    private TMPro.TextMeshProUGUI counterText;
    [SerializeField] public int counter = 3;

    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
        counterText = GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void OnDestroy() => GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;

    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        if (state == GameState.Countdown)
        {
            StartCoroutine(Counter());
        }

        if (state == GameState.Race)
        {
            counterText.gameObject.SetActive(false);
            //Destroy(this);
        }
    }

    IEnumerator Counter()
    {
        while(counter >= 1)
        {
            counterText.text = counter.ToString();
            yield return new WaitForSeconds(1);
            counter -= 1;
        }

        GameManager.Instance.UpdateGameState(GameState.Race);
    }
}
