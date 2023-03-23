using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    public float time = 0f;
    private bool leftKeyPressed = false;
    private bool rightKeyPressed = false;
    
    private float playerSpeed = 0f;
    private float acceleration;
    [SerializeField]
    public float mass = 10f;
    [SerializeField]
    public float force = 0.1f;

    public SerialController serialController;
    public TMPro.TextMeshProUGUI countdown;

    void Awake()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
    }
    void Update()
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

    void OnMessageArrived(string msg)
    {
        string message = serialController.ReadSerialMessage();
        //Debug.Log(message);
        //Debug.Log(msg);
        //gameObject.transform.Translate(new Vector3(0f, 0f, playerDistance) * playerSpeed * Time.deltaTime);
        Move();
    }

    private void Move()
    {
        acceleration = force / mass;
        playerSpeed += acceleration * (1-time);
        Debug.Log(playerSpeed);
        gameObject.transform.Translate(0, 0, playerSpeed * Time.deltaTime);
    }
}
 