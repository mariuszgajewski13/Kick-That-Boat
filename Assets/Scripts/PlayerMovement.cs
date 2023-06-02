using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private float timeBetweenKeys;
    public SerialController serialController;
    
    [SerializeField] private float mass;
    [SerializeField] private float force;
    
    public InputAction player1Input;
    public InputAction player2Input;
    
    [SerializeField] private Rigidbody player1Rigidbody;
    [SerializeField] private Rigidbody player2Rigidbody;
    
    private float playerSpeed;
    private float acceleration;
    
    private bool leftKeyPressed;
    private bool rightKeyPressed;
    
    public Slider slider;
    
    private void Awake()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
        leftKeyPressed = false;
        rightKeyPressed = false;
        
        player1Input.Enable();
        player2Input.Enable();
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    private void Update()
    {
        if (GameManager.instance.race)
        {
            //if (player1Input.WasPerformedThisFrame())
            //{
                CheckInput(Input.GetKeyDown(KeyCode.LeftArrow), Input.GetKeyDown(KeyCode.RightArrow), player1Rigidbody);
            //}
            
            if (player2Input.triggered)
            {
                CheckInput(Input.GetKeyDown(KeyCode.A), Input.GetKeyDown(KeyCode.D), player2Rigidbody);
            }

        }
    }

    private void GameManagerOnOnGameStateChanged(GameState state)
    {

    }

    /*void OnMessageArrived(string msg)
    {
        msg = serialController.ReadSerialMessage();
        Debug.Log(msg);
        Move();
    }*/

    private void Move(Rigidbody player)
    {
        acceleration = force / mass;
        playerSpeed += acceleration * (1-timeBetweenKeys);
        //Debug.Log(playerSpeed);
        if(player == player1Rigidbody)
            player1Rigidbody.AddRelativeForce(new Vector3(0, 0, 1) * playerSpeed , ForceMode.Acceleration);
        if (player == player2Rigidbody)
            player2Rigidbody.AddRelativeForce(new Vector3(0, 0, 1) * playerSpeed, ForceMode.Acceleration);

    }

    private void CheckInput(bool left, bool right, Rigidbody player)
    {
        timeBetweenKeys += Time.deltaTime;
        slider.value = 1-timeBetweenKeys;

        if (left) 
        {
            leftKeyPressed = true;
            if(rightKeyPressed)
                Move(player);
            rightKeyPressed = false;
            timeBetweenKeys = 0;
        }

        if (right && leftKeyPressed)
        {
            Move(player);
            rightKeyPressed = true;
            leftKeyPressed= false;
            timeBetweenKeys = 0;
        }

        if (timeBetweenKeys > 1)
        {
            leftKeyPressed = false;
            rightKeyPressed = false;
            timeBetweenKeys = 0;
            playerSpeed = 0f;
        }
    }
}
 