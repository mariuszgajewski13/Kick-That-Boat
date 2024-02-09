using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public void LoadMainScene()
    {
        SceneManager.LoadScene("Game scene");
    }

    public void LoadMenu() => SceneManager.LoadScene("Main Menu");
    
    public void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void StartRace() => GameManager.instance.UpdateGameState(GameState.Countdown);
    
    public void QuitGame() => Application.Quit();

}
