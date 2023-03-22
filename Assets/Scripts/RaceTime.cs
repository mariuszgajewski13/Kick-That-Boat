using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RaceTime : MonoBehaviour
{
    private float time = 0f;
    private TMPro.TextMeshProUGUI timer;

    private void Start()
    {
        timer = GetComponent<TMPro.TextMeshProUGUI>();
    }

    void FixedUpdate()
    {
        time += Time.deltaTime;
        timer.text = time.ToString();
    }
}
