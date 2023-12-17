using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //Serial
    public Receiver receiver;
    
    //Input
    public InputActionReference left;
    public InputActionReference right;

    public InputActionReference debug;
    private bool leftKeyPressed;
    private bool rightKeyPressed;
    
    //Movement
    public float acceleration = 10.0f;
    public float maxSpeed = 20.0f;
    public float currentSpeed = 0.0f;
    private Rigidbody playerRigidbody;
    
    //Time
    public MovementTime time;
    
    //VFX
    public ParticleSystem ripples;
    public Camera rippleCam;
    private float velocityXZ;
    private Vector3 playerPos;

    public Animator motorAnimation;
    private float animSpeed = 0;
    
    private void Awake()
    {
        leftKeyPressed = false;
        rightKeyPressed = false;

        playerRigidbody = GetComponent<Rigidbody>();
        left.action.Enable();
        right.action.Enable();
        debug.action.Enable();

        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    private void Start()
    {
        CreateRipple(-180, 180, 3, 2, 2, 2);
    }
    
    private void Update()
    {
        if (GameManager.instance.race || GameManager.instance.state == GameState.Tutorial)
        {
            CheckInput(left.action.triggered, right.action.triggered);
            CheckInput(receiver.left, receiver.right);
        }
        
        rippleCam.transform.position = transform.position + Vector3.up * 10;
        Shader.SetGlobalVector("_Player1", transform.position);
        //Shader.SetGlobalVector("_Player2", transform.position);

        velocityXZ = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(playerPos.x, 0, playerPos.z));
        playerPos = transform.position;

        if (velocityXZ > 0.02f && Time.renderedFrameCount % 5 == 0)
        {
            int y = (int)transform.eulerAngles.y;
            CreateRipple(y-90, y+90, 3, 5, 2, 1);
        }

        animSpeed = animSpeed.Map(currentSpeed, maxSpeed, 0, -1);
        motorAnimation.speed = animSpeed;
    }
    private void GameManagerOnOnGameStateChanged(GameState state)
    {

    }
    

    private void Move()
    {
        currentSpeed += acceleration* (1-time.timeBetweenKeys)  * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);

        playerRigidbody.AddRelativeForce(new Vector3(0, 0, 10) * currentSpeed , ForceMode.Acceleration);

        time.UpdateSpeedUI();
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
            currentSpeed = 0f;
        }

        if(debug.action.triggered){
            playerRigidbody.AddRelativeForce(new Vector3(0, 0, 25) * 10 , ForceMode.Acceleration);
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
public static class ExtensionMethods
{
    public static float Map (this float value, float fromSource, float toSource, float fromTarget, float toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }
}