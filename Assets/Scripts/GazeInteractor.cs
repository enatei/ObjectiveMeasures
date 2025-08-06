using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Attachment;

public class GazeInteractor : MonoBehaviour
{
    public float distance = 30f;
    private Interactable currentInteractable;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                if (currentInteractable != interactable)
                {
                    if (currentInteractable != null)
                        currentInteractable.OnGazeExit();

                    currentInteractable = interactable;
                    currentInteractable.OnGazeEnter();
                }
            }
            else
            {
                if (currentInteractable != null)
                {
                    currentInteractable.OnGazeExit();
                    currentInteractable = null;
                }
            }
        }
        else
        {
            if (currentInteractable != null)
            {
                currentInteractable.OnGazeExit();
                currentInteractable = null;
            }
        }
    }
}
