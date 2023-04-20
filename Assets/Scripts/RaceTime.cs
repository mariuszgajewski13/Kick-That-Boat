using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RaceTime : MonoBehaviour
{
    private float time = 0f;
    private TMPro.TextMeshProUGUI timer;
    public bool levelFinished = false;

    public TMPro.TextMeshProUGUI countdown;

    private void Awake()
    {
        timer = GetComponent<TMPro.TextMeshProUGUI>();
    }

    void FixedUpdate()
    {
        if (!countdown.IsActive())
        {
            time += Time.deltaTime;
            timer.text = time.ToString();
            if(levelFinished)
            {
                return;
            }
        }
    }
}
