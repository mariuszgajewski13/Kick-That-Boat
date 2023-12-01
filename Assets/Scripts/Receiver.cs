using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver : MonoBehaviour
{
    public bool left;
    public bool right;

    void OnMessageArrived(string msg)
    {
        left = false;
        right = false;
        Debug.Log(msg);
        if (msg == "1")
        {
            left = true;
        }
            
        if (msg == "2")
        {
            right = true;
        }
    }
    
    void OnConnectionEvent(bool success)
    {

    }
}
