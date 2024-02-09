using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class ReadCSV : MonoBehaviour
{
    [SerializeField] private string fileName = "data.csv"; // Update with your file path
    public TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        ReadAndDisplayData();
    }

    private void ReadAndDisplayData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(filePath))
        {
            string[] allLines = File.ReadAllLines(filePath);
            List<float> sortedFloats = allLines.Select(line =>
            {
                if (float.TryParse(line, out float floatValue))
                {
                    return floatValue;
                }
                else
                {
                    return 0f;
                }
            }).OrderBy(value => value).ToList();
            int linesToShow = Math.Min(10, sortedFloats.Count);
            for (int i = 1; i < linesToShow; i++)
            {
                text.text += $"{i}. {sortedFloats[i]}\n";
            }
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
            sw.WriteLine("Time");
        }
    }
}