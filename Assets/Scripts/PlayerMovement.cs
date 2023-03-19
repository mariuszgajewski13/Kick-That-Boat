using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float time = 0;
    private bool leftKeyPressed = false;
    private bool rightKeyPressed = false;
    public int playerDistance = 10;
    public int playerSpeed = 10;
    public SerialController serialController;

    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
    }
    void Update()
    {
        time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            leftKeyPressed = true;
            time = 0;
            if(rightKeyPressed)
                gameObject.transform.Translate(new Vector3(0f, 0f, playerDistance) * playerSpeed * Time.deltaTime);

            rightKeyPressed = false;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && leftKeyPressed)
        {
            gameObject.transform.Translate(new Vector3(0f, 0f, playerDistance) * playerSpeed * Time.deltaTime);
            time = 0;
            rightKeyPressed = true;
            leftKeyPressed= false;
        }

        if (time > 1)
        {
            leftKeyPressed= false;
            rightKeyPressed = false;
        }
        
    }

    void OnMessageArrived(string msg)
    {
        string message = serialController.ReadSerialMessage();
        Debug.Log(message);
        Debug.Log(msg);
        gameObject.transform.Translate(new Vector3(0f, 0f, playerDistance) * playerSpeed * Time.deltaTime);
    }
}
 