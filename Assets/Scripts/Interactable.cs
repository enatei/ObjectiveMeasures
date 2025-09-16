using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{ 
    private Renderer _renderer;
    private bool isColoring = true;
    private bool isGazedAt = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _renderer = GetComponent<Renderer>();

        _renderer.material.SetColor("_BaseColor", Color.white);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isColoring = !isColoring;
        }
    }

    public void OnGazeEnter()
    {
        if (isColoring)
        { 
            _renderer.material.color = Color.yellow;
        } else
        {
            if (!isGazedAt)
            {
                isGazedAt = true;
                RandomColorManager colorManager = FindFirstObjectByType<RandomColorManager>();
                if (colorManager != null)
                {
                    colorManager.OnGazeHit(gameObject);
                }
            }
        }
  
    }

    public void OnGazeExit()
    {
        if (isColoring)
        {
            _renderer.material.color = Color.white;
        } else
        {
            isGazedAt = false;
        }
    }
}
