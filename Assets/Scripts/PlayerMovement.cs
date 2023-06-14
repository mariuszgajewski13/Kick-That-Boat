using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private float timeBetweenKeys;
    public SerialController serialController;
    
    [SerializeField] private float mass;
    [SerializeField] private float force;
    
    public InputActionReference left;
    public InputActionReference right;
    
     private Rigidbody playerRigidbody;
    
    private float playerSpeed;
    private float acceleration;
    
    private bool leftKeyPressed;
    private bool rightKeyPressed;

    public MovementTime time;
    
    private void Awake()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
        leftKeyPressed = false;
        rightKeyPressed = false;

        playerRigidbody = GetComponent<Rigidbody>();
        
        left.action.Enable();
        right.action.Enable();

        left.action.started += OnInputActionStarted;
        
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    private void OnInputActionStarted(InputAction.CallbackContext obj)
    {
        if (GameManager.instance.race)
        {
            CheckInput(true, true);
        }
    }

    private void Update()
    {
        if (GameManager.instance.race)
        {
            CheckInput(left.action.triggered, right.action.triggered);
        }
    }
    private void GameManagerOnOnGameStateChanged(GameState state)
    {

    }

    void OnMessageArrived(string msg)
    {
        //Debug.Log(msg);
        if (msg == "1")
        {
            OnInputActionStarted(new InputAction.CallbackContext());
        }
        
        if (msg == "2")
        {
            left.action.Disable();
            right.action.Enable();
        }
    }
    
    void OnConnectionEvent(bool success)
    {

    }

    private void Move()
    {
        acceleration = force / mass;
        playerSpeed += acceleration * (1-time.timeBetweenKeys);
        playerRigidbody.AddRelativeForce(new Vector3(0, 0, 1) * playerSpeed , ForceMode.Acceleration);
    }

    private void CheckInput(bool left, bool right)
    {
        if (left) 
        {
            leftKeyPressed = true;
            if(rightKeyPressed)
                Move();
            rightKeyPressed = false;
            time.timeBetweenKeys = 0;
        }

        if (right && leftKeyPressed)
        {
            Move();
            rightKeyPressed = true;
            leftKeyPressed= false;
            time.timeBetweenKeys = 0;
        }

        if (time.timeBetweenKeys > 1)
        {
            leftKeyPressed = false;
            rightKeyPressed = false;
            time.timeBetweenKeys = 0;
            playerSpeed = 0f;
        }
    }
}
 