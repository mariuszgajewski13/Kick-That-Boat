using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RaceTime : MonoBehaviour
{
    private float time = 0f;
    private TMPro.TextMeshProUGUI timer;
    public TMPro.TextMeshProUGUI finalTimeText;
    public float finalTime;
    public TMPro.TextMeshProUGUI countdown;

    public bool levelFinished = false;


    private void Awake()
    {
        timer = GetComponent<TMPro.TextMeshProUGUI>();
    }

    void FixedUpdate()
    {
        if (!countdown.IsActive())
        {
            time += Time.deltaTime;
            timer.text = time.ToString("N3");
            if(levelFinished)
            {
                finalTime = time;
                timer.gameObject.SetActive(false);
                ShowTime();
            }
        }
    }

    private void ShowTime()
    {
        finalTimeText.gameObject.SetActive(true);
        finalTimeText.text = finalTime.ToString("N3");
        //return;
    }
}
