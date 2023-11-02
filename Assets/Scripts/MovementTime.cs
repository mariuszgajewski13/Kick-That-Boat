using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementTime : MonoBehaviour
{
    public Slider slider;
    
    public float timeBetweenKeys = 0;
    public PlayerMovement player;

    private void Update()
    {
        //slider.value = 0;
        timeBetweenKeys += Time.deltaTime;
        //if (player.GetComponent<Rigidbody>().velocity.z > 1)
        if (timeBetweenKeys != 0)
        {
            slider.value += (1-timeBetweenKeys);
            //slider.value++;
        }
        else
        {
            slider.value--;
            
        }
        
    }
}
