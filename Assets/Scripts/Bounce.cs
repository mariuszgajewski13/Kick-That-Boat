using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public Transform[] floaters;
    
    public float underWaterDrag = 3f;
    public float underWaterAngularDrag = 1f;
    public float airDrag = 0f;
    public float airAngularDrag = 0.05f;
    public float floatingPower = 15f;
    public float waterHeight = 0f;

    private Rigidbody rb;
    private bool underwater;
    private int floatersUnderwater;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        floatersUnderwater = 0;
        for(int i=0;i<floaters.Length;i++)
        {
            float difference = floaters[i].position.y - waterHeight;

            if (difference < 0)
            {
                rb.AddForceAtPosition(Vector3.up * floatingPower * MathF.Abs(difference), floaters[i].position, ForceMode.Force);
                floatersUnderwater += 1;
                if (!underwater)
                {
                    underwater = true;
                    SwitchState(true);
                }
            }
            else if (underwater && floatersUnderwater == 0)
            {
                underwater = false;
                SwitchState(false);
            }
            
        }
    }

    void SwitchState(bool isUnderwater)
    {
        if (underwater)
        {
            rb.drag = underWaterDrag;
            rb.angularDrag = underWaterAngularDrag;
        }
        else
        {
            rb.drag = airDrag;
            rb.angularDrag = airAngularDrag;
        }
    }
}
     
