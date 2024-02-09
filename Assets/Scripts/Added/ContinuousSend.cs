using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousSend : MonoBehaviour
{
    private SerialController serialController;
   // public string signal;
    // Start is called before the first frame update
    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
    }

    // Update is called once per frame
    void Update()
    {
        //In the case we need some criteria to decide how often to update our physical world
        if (Time.frameCount % 10 == 0)
        {
            //    //  Debug.Log("Toggling Built-in LED");
            //    Debug.Log("1," + Mathf.Round(transform.position.z) + ",1");
            //    serialController.SendSerialMessage("1,"+ Mathf.Round(transform.position.z) + ",0");
            //}


            //  Debug.Log("Toggling Built-in LED");
            //  Debug.Log("1," + Mathf.Round(transform.position.z) + ",1");
            serialController.SendSerialMessage(Mathf.Abs(Mathf.Round(transform.position.y)) + ","
                                                + Mathf.Round(transform.position.z) + ","
                                                + Mathf.Abs(Mathf.Round(transform.position.x)));
            Debug.Log(Mathf.Abs(Mathf.Round(transform.position.y)) + ","
                                            + Mathf.Round(transform.position.z) + ","
                                            + Mathf.Abs(Mathf.Round(transform.position.x)));
        }
    }
}
