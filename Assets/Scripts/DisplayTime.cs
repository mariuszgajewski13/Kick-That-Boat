using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisplayTime : MonoBehaviour
{
    public TMPro.TextMeshProUGUI textMeshProUGUI;
    public PlayerMovement playerMovement;

    private void Update()
    {
        textMeshProUGUI.text = playerMovement.time.ToString();
    }
}
