using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GazeLineRenderer : MonoBehaviour
{
    public float distance;
    LineRenderer lineRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.positionCount = 2;

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
