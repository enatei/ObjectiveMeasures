using UnityEngine;
using TMPro;

public class EyeBlinkCounter : MonoBehaviour
{
    public TextMeshProUGUI blinkCounterText; 
    private int blinkCount = 0;             
    private bool isBlinking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float yScale = transform.localScale.y;

        if (yScale < 0.0001f)
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
