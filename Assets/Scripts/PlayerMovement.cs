using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float timeBetweenKeys;
    public SerialController serialController;
    
    [SerializeField] private float mass;
    [SerializeField] private float force;
    
    public InputAction player1Input;
    public InputAction player2Input;
    

    

    private Rigidbody player1Rigidbody;
    private Rigidbody player2Rigidbody;
    
    private float playerSpeed;
    private float acceleration;
    
    private bool leftKeyPressed;
    private bool rightKeyPressed;

    private void Awake()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
        player1Rigidbody = GetComponent<Rigidbody>();
        leftKeyPressed = false;
        rightKeyPressed = false;
        player1Input.Enable();
        player2Input.Enable();
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }
    
    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        if (state == GameState.Race)
        {
            timeBetweenKeys += Time.deltaTime;
            
            if (player1Input.triggered) 
            {
                Debug.Log("trigger");
                if (player1Input.ReadValue<float>() != 0)
                {
                    Debug.Log("float");
                    float timeSinceLastKeyPress = Time.time - timeBetweenKeys;
                    timeBetweenKeys = Time.time;
                    Move(player1Rigidbody);
                }
                //leftKeyPressed = true;
                //if(rightKeyPressed)
                //rightKeyPressed = false;
                //timeBetweenKeys = 0;
            }

            /*if (Input.GetKeyDown(KeyCode.RightArrow) && leftKeyPressed)
            {
                Move(player1Rigidbody);
                rightKeyPressed = true;
                leftKeyPressed= false;
                timeBetweenKeys = 0;
            }

            if (timeBetweenKeys > 1)
            {
                leftKeyPressed= false;
                rightKeyPressed = false;
                timeBetweenKeys = 0;
                playerSpeed = 0f;
            }*/
        }
    }


    /*void OnMessageArrived(string msg)
    {
        msg = serialController.ReadSerialMessage();
        Debug.Log(msg);
        Move();
    }*/

    private void Move(Rigidbody player)
    {
        /*acceleration = force / mass;
        playerSpeed += acceleration * (1-timeBetweenKeys);
        //Debug.Log(playerSpeed);
        if(player == player1Rigidbody)
            player1Rigidbody.AddRelativeForce(new Vector3(0, 0, 1) * playerSpeed, ForceMode.Acceleration);
        if (player == player2Rigidbody)
            player2Rigidbody.AddRelativeForce(new Vector3(0, 0, 1) * playerSpeed, ForceMode.Acceleration);*/
        // Example movement handling
        float baseMovementSpeed = 5f; // Adjust this as needed

// Calculate a speed multiplier based on the time between key presses
        float speedMultiplier = 1f + (1f / timeBetweenKeys);

// Calculate the movement distance based on the movement speed and time between key presses
        float movementSpeed = baseMovementSpeed * speedMultiplier;
        float movementDistance = movementSpeed * Time.deltaTime;

// Apply the movement based on the calculated distance
        player.AddRelativeForce(new Vector3(0, 0, movementDistance) * movementSpeed, ForceMode.Acceleration);
    }
}
 