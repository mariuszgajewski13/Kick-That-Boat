using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private  MessageListener messageListener;
    public float speed;//= 5.0f;
    public float turnSpeed;
    public float horizontalInput;
    public float verticalInput;
    public float roll, pitch;
    // public float[] receivedValues;

    // Start is called before the first frame update
    void Start()
    {
        // receivedValues = new float[3];
        messageListener = GameObject.Find("SerialController").GetComponent<MessageListener>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //horizontalInput = Input.GetAxis("Horizontal");
        //verticalInput = Input.GetAxis("Vertical");
        pitch = messageListener.ReceivePitch();
        roll = messageListener.ReceiveRoll();
        //move
        //  transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
        if (Mathf.Abs(pitch)>1) transform.Translate(Vector3.forward * Time.deltaTime * speed * pitch);
        // transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * horizontalInput);
        //transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);
        if (Mathf.Abs(roll)>1) transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * roll);
    }
}
