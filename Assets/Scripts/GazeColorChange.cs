using UnityEngine;

public class GazeColorChanger : MonoBehaviour
{
    public Color gazeColor = Color.yellow;

    private Color originalColor;
    private MeshRenderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        //if (meshRenderer != null)
            //originalColor = meshRenderer.material.color;
    }

    // Wird beim Gaze Hover aufgerufen
    public void OnGazeEnter()
    {
        //if (meshRenderer != null)
            //meshRenderer.material.color = gazeColor;
    }

    // Wird beim Gaze Hover verlassen aufgerufen
    public void OnGazeExit()
    {
        //if (meshRenderer != null)
            //meshRenderer.material.color = originalColor;
    }
}

