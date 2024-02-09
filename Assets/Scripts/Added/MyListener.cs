using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyListener : MonoBehaviour
{
    GameObject cubeModifier;
    // Start is called before the first frame update
    void Start()
    {
        cubeModifier = GameObject.Find("Cube");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMessageArrived(string msg)
    {
        Debug.Log("moving at speed: " + msg);
        float speed = float.Parse(msg) * 100;
        cubeModifier.gameObject.transform.Rotate(Vector3.up * Time.deltaTime * speed);
    }
    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
}