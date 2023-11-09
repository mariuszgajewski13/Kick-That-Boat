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

    private void Start()
    {
        slider.maxValue = player.maxSpeed;
    }

    private void Update()
    {
        timeBetweenKeys += Time.deltaTime;

        if (player.currentSpeed <= 0)
            slider.value --;
    }
    
    public void UpdateSpeedUI() => slider.value += player.currentSpeed / player.maxSpeed;

}
