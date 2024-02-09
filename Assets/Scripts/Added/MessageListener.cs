using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageListener : MonoBehaviour
{
    private SerialController serialController;
    private bool calibration = true;
    const int values = 3;
    private float[] calibValues;
    private float[] receivedValues;

    // Start is called before the first frame update
    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
        calibValues = new float[values];
        receivedValues = new float[values];
    }


    void Update()
    {

        //---------------------------------------------------------------------
        // Send data
        //---------------------------------------------------------------------

        // If you press one of these keys send it to the serial device. A
        // sample serial device that accepts this input is given in the README.
        //if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        //{
        //    Debug.Log("Sending lights ON");
        //    serialController.SendSerialMessage("A");
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        //{
        //    Debug.Log("Sending lights OFF");
        //    serialController.SendSerialMessage("Z");
        //}


        //---------------------------------------------------------------------
        // Receive data
        //---------------------------------------------------------------------

        string message = serialController.ReadSerialMessage();

        if (message == null)
            return;

        // Check if the message is plain data or a connect/disconnect event.
        if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
        {
            Debug.Log("Connection established");
            calibration = true;
        }

        else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
        {
            Debug.Log("Connection attempt failed or disconnection detected");
            calibration = true;
        }
        else
        {

            string[] stringValues = message.Split(',');
            float[] floatValues = new float[stringValues.Length];
            if (floatValues.Length == values)
            {         //we check that the string was received correctly
                for (int i = 0; i < values; ++i)
                    floatValues[i] = float.Parse(stringValues[i].Trim());

                if (calibration)
                {
                    calibration = false;
                    for (int i = 0; i < values; ++i)
                    {
                        calibValues[i] = floatValues[i];
                    }
                    Debug.Log("calibrated!");
                }
                else
                {
                    for (int i = 0; i < values; ++i)
                        receivedValues[i] = floatValues[i] - calibValues[i];
                  //  Debug.Log("pitch: " + receivedValues[0] + " roll: " + receivedValues[1] + " yaw: " + receivedValues[2]);
                }
            }
        }

    }

    public float ReceivePitch()
    {
        return receivedValues[0];
    }

    public float ReceiveRoll()
    {
        return receivedValues[1];
    }
}
