using UnityEngine;

public class Receiver2 : MonoBehaviour
{
    public bool left;
    public bool right;

    void OnMessageArrived(string msg)
    {
        left = false;
        right = false;
        //Debug.Log(msg);
        switch (msg)
        {
            case "3":
                left = true;
                right = false;
                break;
            case "4":
                right = true;
                left = false;
                break;
        }
    }
    
    void OnConnectionEvent(bool success)
    {

    }
}