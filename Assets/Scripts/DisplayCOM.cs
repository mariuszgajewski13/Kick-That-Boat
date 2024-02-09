using TMPro;
using UnityEngine;
using System.IO;

public class DisplayCOM : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private SerialController _serialController;
    public TMP_Dropdown dropdown;
    public GameObject com;
    [SerializeField] private string fileName = "port.txt";
    string filePath;
    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, fileName);
        _text = GetComponent<TextMeshProUGUI>();
        _serialController = com.GetComponent<SerialController>();
        ReadFile();
        Display();
    }

    public void Change()
    {
        int port = dropdown.value;
        _serialController.portName = dropdown.options[port].text;
        PrefabData.Instance.yourValueChanged = _serialController.portName;
        _serialController.portName = PrefabData.Instance.yourValueChanged;
        WriteToFile(port);
        Display();
    }

    private void Display()
    {
        _text.text = $"Platform connected to port: \n{_serialController.portName}";
    }

    private void WriteToFile(int port)
    {
        if (File.Exists(filePath))
        {
            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.WriteLine(dropdown.options[port].text);
            }
        }
        else
        {
            CreateFile(filePath);
        }
    }

    private void ReadFile()
    {
        if (File.Exists(filePath))
        {
            _serialController.portName = File.ReadAllLines(filePath)[0];
        }
        else
        {
            CreateFile(filePath);
        }
    }
    
    private void CreateFile(string filePath)
    {
        using (StreamWriter sw = File.CreateText(filePath))
        {
            sw.WriteLine();
        }
    }
}
