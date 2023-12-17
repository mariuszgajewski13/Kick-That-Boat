using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public Button[] buttons;

    public InputActionReference player1Left;
    public InputActionReference player1Right;
    public InputActionReference player2Left;
    public InputActionReference player2Right;

    public Receiver player1;
    //public Receiver player2;
    
    private Button activeButton;
    
    private int tapCount = 0;
    private float lastTapTime = 0f;
    public float multiTapDelay = 0.8f;

    private int activeButtonIndex;
    
    private void Start()
    {
        player1Left.action.Enable();
        player1Right.action.Enable();
        player2Left.action.Enable();
        player2Right.action.Enable();

        buttons[0].Select();
        activeButton = buttons[0];
    }

    private void Update()
    {
        activeButtonIndex = Array.IndexOf(buttons, activeButton);
        if (player1.left ||
            player1Left.action.triggered)
        {
            if (activeButton == buttons[0])
                activeButton = buttons[0];
            else
            {
                activeButton = buttons[activeButtonIndex - 1];
                activeButton.Select();
            }
        }

        if (player1.right ||
            player1Right.action.triggered)
        {
            if (activeButton == buttons[buttons.Length - 1])
                activeButton = buttons[buttons.Length - 1];
            else
            {
                activeButton = buttons[activeButtonIndex + 1];
                activeButton.Select();
            }

        }

        if (activeButton == buttons[0])
            if (DoubleTap(player1Left.action.triggered, player1Left.action.triggered))
                Select();

        if (activeButton == buttons[buttons.Length - 1])
            if (DoubleTap(player1Right.action.triggered, player1Right.action.triggered))
                Select();
        //////////////////////////////////////
        // if (player2.left ||
        //     player2Left.action.triggered)
        // {
        //     if (activeButton == buttons[0])
        //         activeButton = buttons[0];
        //     else
        //     {
        //         activeButton = buttons[activeButtonIndex - 1];
        //         activeButton.Select();
        //     }
        // }

        // if (player2.right ||
        //     playerRight.action.triggered)
        // {
        //     if (activeButton == buttons[buttons.Length - 1])
        //         activeButton = buttons[buttons.Length - 1];
        //     else
        //     {
        //         activeButton = buttons[activeButtonIndex + 1];
        //         activeButton.Select();
        //     }

        // }

        // if (activeButton == buttons[0])
        //     if (DoubleTap(player2Left.action.triggered, player2Left.action.triggered))
        //         Select();

        // if (activeButton == buttons[buttons.Length - 1])
        //     if (DoubleTap(player2Right.action.triggered, player2Right.action.triggered))
        //         Select();
            
    }

    void Select() => activeButton.onClick.Invoke();

    bool DoubleTap(bool p1, bool p2)
    {
        if (p1 || p2)
        {
            float timeSinceLastTap = Time.time - lastTapTime;
            
            if (timeSinceLastTap <= multiTapDelay)
            {
                tapCount++;
            }
            else
            {
                tapCount = 1;
            }

            lastTapTime = Time.time;

            if (tapCount >= 2)
            {
                return true;
            }

        }
            
        return false;
    }
}
