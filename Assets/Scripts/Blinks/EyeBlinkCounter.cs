using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

public class EyeBlinkCounter : MonoBehaviour
{
    public TextMeshProUGUI blinkCounterText;
    public GameObject rightEyeGeometric;
    public GameObject leftEyeGeometric;
    private int blinkCount = 0;             
    private bool isBlinking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float yScaleRight = rightEyeGeometric.transform.localScale.y;
        float yScaleLeft = leftEyeGeometric.transform.localScale.y;
        float blinkThreshold = 0.000001f;

        bool bothEyesClosed = yScaleRight < blinkThreshold && yScaleLeft < blinkThreshold;

        if (bothEyesClosed)
        {
            if (!isBlinking)
            {
                blinkCount++;
                isBlinking = true;
            }
        }
        else
        {
            isBlinking = false;
        }

        if (blinkCounterText != null)
        {
            blinkCounterText.text = "" + blinkCount;
        }
    }
}
