using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GazeLineRenderer : MonoBehaviour
{
    public float distance = 50f;
    public LineRenderer lineRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Vector3 endPoint = ray.origin + ray.direction * distance;

        if (Physics.Raycast(ray, out RaycastHit hit, distance))
        {
            //endPoint = hit.point;
        }

        lineRenderer.SetPosition(0, ray.origin);
        lineRenderer.SetPosition(1, endPoint);
    }
}
