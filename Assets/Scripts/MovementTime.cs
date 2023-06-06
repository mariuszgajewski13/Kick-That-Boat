using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementTime : MonoBehaviour
{
    public Slider slider;
    
    public float timeBetweenKeys = 0;

    private void Update()
    {
        timeBetweenKeys += Time.deltaTime;
        slider.value = 1-timeBetweenKeys;
    }
}
