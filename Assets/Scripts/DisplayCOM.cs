using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCOM : MonoBehaviour
{
    private TextMeshProUGUI _text;
    public SerialController com;
    public TMP_Dropdown dropdown;
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        Display();
    }

    public void Change()
    {
        int port = dropdown.value;
        com.portName = dropdown.options[dropdown.value].text;
        Display();
    }

    private void Display()
    {
        _text.text = "Platform connectedd to port: \n" + com.portName;
    }
}
