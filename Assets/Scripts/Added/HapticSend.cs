using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticSend : MonoBehaviour
{
    private SerialController serialController;
    public string signal;
    // Start is called before the first frame update
    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
    }

    // Update is called once per frame
    void Update()
    {

        // If you press one of these keys send it to the serial device. A
        // sample serial device that accepts this input is given in the README.
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            Debug.Log("Sending lights ON");
            //signal = "A";
            serialController.SendSerialMessage("A");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            Debug.Log("Sending lights OFF");
            //signal = "B";
            serialController.SendSerialMessage("B");
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
        if (collision.relativeVelocity.magnitude > 2)
            serialController.SendSerialMessage(signal);
    }
}
