using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    //public Transform[] floaters;
    
    //public float underWaterDrag = 3f;
    //public float underWaterAngularDrag = 1f;
    public float airDrag = 0f;
    public float airAngularDrag = 0.05f;
    //public float floatingPower = 15f;
    //public float waterHeight = 0f;
    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;
    public float floaterCount = 1;

    public Rigidbody rb;
    //private bool underwater;
    //private int floatersUnderwater;

    private void FixedUpdate()
    {
        //rb.AddForceAtPosition(Physics.gravity, transform.position, ForceMode.Acceleration);
        float waveHeight = WaveManager.instance.GetWaveHeight(transform.position.z);
        if (transform.position.y < waveHeight)
        {
            float displacementMultiplier = Mathf.Clamp01((waveHeight - transform.position.y) / depthBeforeSubmerged) * displacementAmount;
            rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f),transform.position, ForceMode.Acceleration);
            rb.AddForce(-rb.velocity * (displacementMultiplier * airDrag * Time.fixedDeltaTime), ForceMode.VelocityChange);
            rb.AddTorque(-rb.angularVelocity * (displacementMultiplier * airAngularDrag * Time.fixedDeltaTime), ForceMode.VelocityChange);
        }
        // floatersUnderwater = 0;
        // for(int i=0;i<floaters.Length;i++)
        // {
        //     float difference = floaters[i].position.y - waterHeight;
        //
        //     if (difference < 0)
        //     {
        //         //rb.AddForceAtPosition(Vector3.up * floatingPower * MathF.Abs(difference), floaters[i].position, ForceMode.Force);
        //         rb.AddForceAtPosition(Physics.gravity / floaters.Length, transform.position, ForceMode.Acceleration);
        //
        //         float waveHeight = WaveManager.instance.GetWaveHeight(transform.position.x);
        //         if (transform.position.y < waveHeight)
        //         {
        //             float displacementMultiplier = Mathf.Clamp01((waveHeight - transform.position.y) / depthBeforeSubmerged);
        //             rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);
        //             rb.AddForce(displacementMultiplier * -rb.velocity * airDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        //             rb.AddTorque(displacementMultiplier * -rb.angularVelocity * airAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        //             
        //             
        //         }
        //         
        //         floatersUnderwater += 1;
        //         if (!underwater)
        //         {
        //             underwater = true;
        //             SwitchState(true);
        //         }
        //     }
        //     else if (underwater && floatersUnderwater == 0)
        //     {
        //         underwater = false;
        //         SwitchState(false);
        //     }
            
        //}
    }

    // void SwitchState(bool isUnderwater)
    // {
    //     if (isUnderwater)
    //     {
    //         rb.drag = underWaterDrag;
    //         rb.angularDrag = underWaterAngularDrag;
    //     }
    //     else
    //     {
    //         rb.drag = airDrag;
    //         rb.angularDrag = airAngularDrag;
    //     }
    // }
}
     
