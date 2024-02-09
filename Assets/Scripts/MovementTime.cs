using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MovementTime : MonoBehaviour
{
    public Slider slider;
    public float timeBetweenKeys = 0;
    public PlayerMovement player;
    
    public List<float> LtimeBetweenPresses = new List<float>();
    public List<float> LavgSpeed = new List<float>();
    public float timeBetweenPresses;
    public float avgSpeed;

    private void Start()
    {
        slider.maxValue = player.maxSpeed;
    }

    private void Update()
    {
        timeBetweenKeys += Time.deltaTime;
        
        if (player.currentSpeed <= 0)
            slider.value --;
        
        
        LtimeBetweenPresses.Add(timeBetweenKeys);
        LavgSpeed.Add(player.currentSpeed);

        timeBetweenPresses = LtimeBetweenPresses.Average();
        avgSpeed = LavgSpeed.Average();
    }
    
    public void UpdateSpeedUI() => slider.value += player.currentSpeed / player.maxSpeed;

}
