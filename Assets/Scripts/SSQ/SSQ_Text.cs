using System;
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using System.IO;
using static UnityEngine.Rendering.DebugUI;

public class SSQ_Text : MonoBehaviour
{

    public TextMeshProUGUI SSQquestions;
    public bool isPreExposure;
    private string[] questions;
    private string[] answers;
    private int questionCounter;
    private bool isAnswered;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        questionCounter = 0;
        isAnswered = false;
        questions = new string[] { 
            "General Discomfort",
            "Fatigue",
            "Headache",
            "Eye Strain",
            "Difficulty Focusing",
            "Increased Salvation",
            "Sweating",
            "Nausea",
            "Difficulty Concentrating",
            "Fullness of Head",
            "Blurred Vision",
            "Dizzy (with eyes open)",
            "Dizzy (with eyes closed)",
            "Vertigo",
            "Stomach Awareness",
            "Burping"
        };
        answers = new string[questions.Length];
        UpdateText();
    }

    // Update is called once per frame
    void UpdateText()
    {
        if (!isAnswered)
        {
            SSQquestions.text = "Are you experiencing" + "\n"
                + " " + questions[questionCounter] + "\n"
                + " at the moment?";
        }
        else
        {
            SSQquestions.text = "Thank you. The questionnaire has been completed.";
        }
        
    }

    public void OnAnswerPressed(int value)
    {
        GameObject button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        StartCoroutine(WaitAndUpdate(button, value));
    }

    private IEnumerator WaitAndUpdate(GameObject button, int value)
    {
        answers[questionCounter] = value.ToString();
        questionCounter++;

        yield return new WaitForSeconds(1f);

        if (questionCounter == questions.Length)
        {
            isAnswered = true;
            WriteResults();
        }

        UpdateText();
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
    }


    private void WriteResults()
    {
        string folderPath;

        if (isPreExposure)
        {
            folderPath = Path.Combine(Application.dataPath, "Pre");

        } else
        {
            folderPath = Path.Combine(Application.dataPath, "Post");
        }

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        string path = Path.Combine(folderPath, "SSQ_Results.csv");

        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.WriteLine(string.Join(",", questions));
            writer.WriteLine(string.Join(",", answers));
            writer.Close();
        }
    }
}
