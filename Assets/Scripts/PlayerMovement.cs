using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float time;
    public SerialController serialController;
    public TMPro.TextMeshProUGUI countdown;
    [SerializeField] private float mass;
    [SerializeField] private float force;

    private float playerSpeed;
    private float acceleration;
    private bool leftKeyPressed;
    private bool rightKeyPressed;
    private Rigidbody rb;
    private RaceTime isFinished;

    private void Awake()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
        rb = this.GetComponent<Rigidbody>();
        leftKeyPressed = false;
        rightKeyPressed = false;
    }
    private void Update()
    {
        time += Time.deltaTime;
        
        if(!countdown.IsActive())
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) 
            {
                leftKeyPressed = true;
                if(rightKeyPressed)
                    Move();

                rightKeyPressed = false;
                time = 0;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) && leftKeyPressed)
            {
                Move();
                rightKeyPressed = true;
                leftKeyPressed= false;
                time = 0;
            }

            if (time > 1)
            {
                leftKeyPressed= false;
                rightKeyPressed = false;
                time = 0;
                playerSpeed = 0f;
            }
        }
    }

    /*void OnMessageArrived(string msg)
    {
        msg = serialController.ReadSerialMessage();
        Debug.Log(msg);
        Move();
    }*/

    private void Move()
    {
        acceleration = force / mass;
        playerSpeed += acceleration * (1-time);
        Debug.Log(playerSpeed);
        rb.AddForce(new Vector3(0, 0, 1) * playerSpeed, ForceMode.Acceleration);
    }
}
 