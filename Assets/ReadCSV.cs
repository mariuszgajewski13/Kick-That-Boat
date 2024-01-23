using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class ReadCSV : MonoBehaviour
{
    [SerializeField] private string filePath = "Assets/data.csv"; // Update with your file path
    public TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        ReadAndDisplayData();
    }

    private void ReadAndDisplayData()
    {
        try
        {
            if (File.Exists(filePath))
            {
                string[] allLines = File.ReadAllLines(filePath);
                List<string> sortedLines = allLines.OrderByDescending(line => line).ToList();
                int linesToShow = Math.Min(10, sortedLines.Count);
                for (int i = 1; i < linesToShow; i++)
                {
                    text.text += $"{i}. {sortedLines[i]}\n";
                }
            }
            else
            {
                Debug.LogError("File not found: " + filePath);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error reading file: " + e.Message);
        }
    }
}