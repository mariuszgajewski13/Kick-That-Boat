using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadMainScene() => SceneManager.LoadScene(1);

    public void LoadMenu() => SceneManager.LoadScene(0);
    
    public void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void QuitGame() => Application.Quit();
}
