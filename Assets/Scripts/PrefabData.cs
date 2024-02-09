using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabData : MonoBehaviour
{
    public static PrefabData Instance;

    public string yourValueChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}