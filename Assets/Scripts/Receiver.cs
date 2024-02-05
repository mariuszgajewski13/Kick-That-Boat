using UnityEngine;

public class Receiver : MonoBehaviour
{
    public bool left;
    public bool right;

    void OnMessageArrived(string msg)
    {
        left = false;
        right = false;
        switch (msg)
        {
            case "1":
                left = true;
                break;
            case "2":
                right = true;
                break;
        }
    }
    
    void OnConnectionEvent(bool success)
    {

    }
}
