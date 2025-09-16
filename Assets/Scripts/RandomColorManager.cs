using System;
using System.IO;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using System.Security.Cryptography;
using UnityEngine.InputSystem;

public class RandomColorManager : MonoBehaviour
{

    private bool isColoring = false;
    private GameObject currentTarget;
    private Color targetColor = Color.yellow;
    private float timer = 0f;
    private bool timerRunning = false;
    private int objectCount = 0;
    private string logFilePath;
    private bool hasBeenHit = false;
    public int maxObjects = 5;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string projectPath = Application.dataPath; 
        string logFolderPath = Path.Combine(projectPath, "Logs");

        if (!Directory.Exists(logFolderPath))
        {
            Directory.CreateDirectory(logFolderPath);
        }

        logFilePath = Path.Combine(logFolderPath, "gaze_log.txt");

        Debug.Log("Log-Data is saved under: " + logFilePath);

    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isColoring = !isColoring;
        }
        
        if (isColoring)
        {
            if (currentTarget == null && objectCount < maxObjects)
            {
                SelectNewTarget();
            }

            if (timerRunning)
            {
                timer += Time.deltaTime;
            }

        }
    }

    void SelectNewTarget()
    {
        if (currentTarget == null)
        {
            int newNumber = UnityEngine.Random.Range(1, 7);
            String name = "Sphere" + newNumber;

            GameObject newTarget = GameObject.Find(name);
            if (newTarget != null) 
            {
                currentTarget = newTarget;
                timer = 0f;
                hasBeenHit = false;
                Renderer renderer = currentTarget.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = targetColor;
                    timerRunning = true;
                    objectCount++;
                }
            }
        }
    }

    public void OnGazeHit(GameObject hitObject)
    {
        if (hitObject == currentTarget && !hasBeenHit)
        {
            hasBeenHit = true;
            timerRunning = false;
            string logLine = "Target " + objectCount + ":\nTarget " + currentTarget.name + " has been hit after " + timer + " seconds.";
            File.AppendAllText(logFilePath, logLine + "\n");
            ResetAllColors();
            StartCoroutine(WaitAndSelectNewTarget(3f));
        }
    }

    IEnumerator WaitAndSelectNewTarget(float time)
    {
        yield return new WaitForSeconds(time);
        currentTarget = null;
    }

    public void ResetAllColors()
    {
        for (int i = 1; i < 7; i++)
        {
            String name = "Sphere" + i;
            GameObject obj = GameObject.Find(name);

            if (obj != null)
            {
                obj.GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }
}
