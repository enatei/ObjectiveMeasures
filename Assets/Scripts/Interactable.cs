using UnityEngine;

public class Interactable : MonoBehaviour
{ 
    private Renderer _renderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _renderer = GetComponent<Renderer>();

        _renderer.material.SetColor("_BaseColor", Color.white);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGazeEnter()
    {
        _renderer.material.SetColor("_BaseColor", Color.yellow);
    }

    public void OnGazeExit()
    {
        _renderer.material.SetColor("_BaseColor", Color.white);
    }
}
