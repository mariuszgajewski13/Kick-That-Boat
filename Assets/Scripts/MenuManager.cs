using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.HID;

public class MenuManager : MonoBehaviour, ISelectHandler
{
    public InputActionReference[] input;
    public void Update()
    {
        if ((input[0].action.triggered && input[1].action.triggered) ||
            (input[2].action.triggered && input[3].action.triggered))
        {
            Debug.Log("Click");
            
        }
    }
    
    public void LoadMainScene() => SceneManager.LoadScene(1);

    public void LoadMenu() => SceneManager.LoadScene(0);
    
    public void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void StartRace() => GameManager.instance.UpdateGameState(GameState.Countdown);

    public void QuitGame() => Application.Quit();
    public void OnSelect(BaseEventData eventData)
    {
        if (eventData.selectedObject == FindObjectOfType(typeof(Button)))
        {
            Debug.Log(eventData.selectedObject.name);
        }

        /*if ()
        {
            eventData.selectedObject.GetComponent<Button>().onClick.Invoke();
            Debug.Log("Click");
        }*/
    }
}
