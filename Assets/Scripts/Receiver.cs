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
        switch (msg)
        {
            case "1":
                left = true;
                left = false;
                break;
            case "2":
                right = true;
                right = false;
                break;
        }
    }
    
    void OnConnectionEvent(bool success)
    {

    }
}
