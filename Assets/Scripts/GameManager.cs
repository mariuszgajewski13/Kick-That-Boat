using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState state;
    public GameObject startScreen;
    public GameObject countdown;
    public static event Action<GameState> OnGameStateChanged;

    public bool race;
   
    public float fadeTime = 0.5f;
    public float opacity = 255f;
    public GameObject fadeScreen;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.Tutorial);
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.Tutorial:
                HandleTutorial();
                break;
            case GameState.Countdown:
                HandleCountdown();
                break;
            case GameState.Race:
                HandleRace();
                break;
            case GameState.Victory:
                HandleVictory();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    void HandleTutorial()
    {
        StartCoroutine(load(fadeTime, opacity));
    }

    void HandleCountdown()
    {
        startScreen.SetActive(false);
        countdown.SetActive(true);
    }
    
    void HandleVictory()
    {
        race = false;
    }

    void HandleRace()
    {
        race = true;
    }

    IEnumerator load(float fadeTime, float opacity){
        yield return new WaitForSeconds(1);
        while(opacity > 0){
            Color color = fadeScreen.GetComponent<Image>().color;
            // fadeScreen.GetComponent<Image>().color = new Color(0, 0, 0, opacity);
            // opacity-=10;
            opacity = color.a - (fadeTime * Time.deltaTime);
            color = new Color(color.r, color.g, color.b, opacity);
            fadeScreen.GetComponent<Image>().color = color;
            yield return null;
        }
        Destroy(fadeScreen);
    }
}

public enum GameState
{
    Tutorial,
    Countdown,
    Race,
    Victory
}