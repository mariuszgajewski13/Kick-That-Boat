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
    
    public float playerSpeed;
    private float acceleration;
    
    private bool leftKeyPressed;
    private bool rightKeyPressed;

    public MovementTime time;

    public ParticleSystem ripples;

    private float velocityXZ;
    private Vector3 playerPos;

    public Camera rippleCam;
    
    private void Awake()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
        leftKeyPressed = false;
        rightKeyPressed = false;

        playerRigidbody = GetComponent<Rigidbody>();
        
        left.action.Enable();
        right.action.Enable();

        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    private void Start()
    {
        CreateRipple(-180, 180, 3, 2, 2, 2);
    }
    
    private void Update()
    {
        if (GameManager.instance.race)
        {
            CheckInput(left.action.triggered, right.action.triggered);
        }
        
        rippleCam.transform.position = transform.position + Vector3.up * 10;
        Shader.SetGlobalVector("_Player", transform.position);

        velocityXZ = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
            new Vector3(playerPos.x, 0, playerPos.z));
        playerPos = transform.position;

        if (velocityXZ > 0.02f && Time.renderedFrameCount % 5 == 0)
        {
            int y = (int)transform.eulerAngles.y;
            CreateRipple(y-90, y+90, 3, 5, 2, 1);
        }
    }
    private void GameManagerOnOnGameStateChanged(GameState state)
    {

    }

    void OnMessageArrived(string msg)
    {
        if (GameManager.instance.race)
        {
            bool left = false;
            bool right = false;
            Debug.Log(msg);
            if (msg == "1")
            {
               left = true;
            }
            
            if (msg == "2")
            {
                right = true;
            }

            CheckInput(left, right);
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
        Debug.Log(playerSpeed);
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

    private void CreateRipple(int start, int end, int delta, float speed, float size, float lifetime)
    {
        Vector3 forward = ripples.transform.eulerAngles;
        forward.y = start;
        ripples.transform.eulerAngles = forward;
        
        for (int i = start; i < end; i+=delta)
        {
            ripples.Emit(transform.position+ripples.transform.forward * 0.5f, ripples.transform.forward * speed, size, lifetime, Color.white);
            ripples.transform.eulerAngles += Vector3.up * delta;
        }
    }
}
 