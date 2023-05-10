using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTime : MonoBehaviour
{
    private TMPro.TextMeshProUGUI time;
    //private TMPro.TextMeshProUGUI slider;
    public Slider slider;
    public PlayerMovement playerMovement;

    public TMPro.TextMeshProUGUI countdown;

    private void Awake()
    {
        time = GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void Update()
    {
        if (!countdown.IsActive())
        {
            time.text = playerMovement.time.ToString("N3");
            slider.value = playerMovement.time;
        }
    }
}
