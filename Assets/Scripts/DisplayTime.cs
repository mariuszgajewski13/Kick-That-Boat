using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using TMPro;
using UnityEngine;


public class DisplayTime : MonoBehaviour
{
    private TMPro.TextMeshProUGUI time;
    public PlayerMovement playerMovement;

    private void Start()
    {
        time = GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void Update()
    {
        time.text = playerMovement.time.ToString("N3");
    }
}
