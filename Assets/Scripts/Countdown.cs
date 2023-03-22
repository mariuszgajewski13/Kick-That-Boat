using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    private TMPro.TextMeshProUGUI counterText;
    [SerializeField] public int counterLenth = 3;

    void Start()
    {
        counterText = GetComponent<TMPro.TextMeshProUGUI>();
        StartCoroutine(CounterCountdown(counterLenth));
    }

    IEnumerator CounterCountdown(int counter)
    {
        while(counter >= 1)
        {
            counterText.text = counter.ToString();
            yield return new WaitForSeconds(1);
            counter -= 1;
        }
        counterText.gameObject.SetActive(false);
    }
}
