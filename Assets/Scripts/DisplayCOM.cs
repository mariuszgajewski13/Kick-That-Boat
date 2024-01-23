using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCOM : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private SerialController _serialController;
    public TMP_Dropdown dropdown;
    public GameObject com;
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _serialController = com.GetComponent<SerialController>();
        
        Display();
    }

    public void Change()
    {
        int port = dropdown.value;
        _serialController.portName = dropdown.options[dropdown.value].text;
        //PrefabUtility.ApplyPrefabInstance(com, InteractionMode.UserAction);
        PrefabData.Instance.yourValueChanged = _serialController.portName;
        _serialController.portName = PrefabData.Instance.yourValueChanged;
        Display();
    }

    private void Display()
    {
        _text.text = $"Platform connected to port: \n{_serialController.portName}";
    }
}
