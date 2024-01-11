using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement1 : MonoBehaviour
{
    //Serial
    public Receiver2 receiver;
    
    //Input
    public InputActionReference left;
    public InputActionReference right;
    
    private bool _leftKeyPressed;
    private bool _rightKeyPressed;
    
    //Movement
    //[SerializeField]
    public float maxSpeed = 20.0f;
    public float acceleration = 10.0f;
    public float currentSpeed;
    private Rigidbody _playerRigidbody;
    
    //Time
    public MovementTime1 time;
    
    //VFX
    public ParticleSystem ripples;
    public Camera rippleCam;
    private float _velocityXZ;
    private Vector3 _playerPos;
    public ParticleSystem splash;
    public ParticleSystem motorSplash;

    public Animator motorAnimation;
    private float _animSpeed;
    
    private static readonly int Player = Shader.PropertyToID("_Player2");

    private void Awake()
    {
        _leftKeyPressed = false;
        _rightKeyPressed = false;
        
        _playerRigidbody = GetComponent<Rigidbody>();
        
        left.action.Enable();
        right.action.Enable();

        receiver = PrefabData.Instance.GetComponent<Receiver2>();
        
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
        
        _playerPos = gameObject.transform.position;
        rippleCam.transform.position = _playerPos + Vector3.up * 10;
        
        Shader.SetGlobalVector(Player, _playerPos);
            
            
        _velocityXZ = Vector3.Distance(new Vector3(_playerPos.x, 0, _playerPos.z), new Vector3(_playerPos.x, 0, _playerPos.z));

        //if (velocityXZ > 0.02f && Time.renderedFrameCount % 5 == 0)
        if (currentSpeed > 0)
        {
            int y = (int)transform.eulerAngles.y;
            splash.Play();
            motorSplash.Play();
            CreateRipple(y-90, y+90, 3, 5, 2, 1);
        }

        if (currentSpeed <= 0)
        {
            splash.Stop();
            motorSplash.Stop();
        }

        float mappedAnimationSpeed = MapSpeedToAnimationSpeed(currentSpeed);
        motorAnimation.speed = mappedAnimationSpeed;
    }
    
    float MapSpeedToAnimationSpeed(float speed)
    {
        speed = Mathf.Clamp(speed, 0f, maxSpeed);
        float mappedSpeed = Mathf.Lerp(0f, 1f, speed / maxSpeed);
        return mappedSpeed;
    }
    
    private void GameManagerOnOnGameStateChanged(GameState state)
    {

    }
    

    private void Move()
    {
        currentSpeed += acceleration* (1-time.timeBetweenKeys)  * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);

        _playerRigidbody.AddRelativeForce(new Vector3(0, 0, 10) * currentSpeed , ForceMode.Acceleration);
        //playerRigidbody.AddRelativeForce(new Vector3(0, 0, 20) * currentSpeed , ForceMode.Acceleration);
        //_playerRigidbody.AddRelativeForce(new Vector3(0, 0, 1) * currentSpeed , ForceMode.VelocityChange);

        time.UpdateSpeedUI();
    }

    private void CheckInput(bool left, bool right)
    {
        if (left) 
        {
            _leftKeyPressed = true;
            if(_rightKeyPressed)
                Move();
            _rightKeyPressed = false;
            time.timeBetweenKeys = 0;
        }

        if (right && _leftKeyPressed)
        {
            Move();
            _rightKeyPressed = true;
            _leftKeyPressed= false;
            time.timeBetweenKeys = 0;
        }

        if (time.timeBetweenKeys > 1)
        {
            _leftKeyPressed = false;
            _rightKeyPressed = false;
            time.timeBetweenKeys = 0;
            currentSpeed = 0f;
        }
    }

    private void CreateRipple(int start, int end, int delta, float speed, float size, float lifetime)
    {
        Vector3 forward = ripples.transform.eulerAngles;
        forward.y = start;
        ripples.transform.eulerAngles = forward;
        
        for (int i = start; i < end; i+=delta)
        {
            ripples.Emit(_playerPos+ripples.transform.forward * 0.5f, ripples.transform.forward * speed, size, lifetime, Color.white);
            ripples.transform.eulerAngles += Vector3.up * delta;
        }
    }
}
