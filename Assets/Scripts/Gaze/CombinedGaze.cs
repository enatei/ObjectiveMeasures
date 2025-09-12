using UnityEngine;
using VIVE.OpenXR.EyeTracker;
using VIVE.OpenXR;

public class CombinedGaze : MonoBehaviour
{

    public Camera camera;
    public float distance;
    LineRenderer lineRenderer;
    private Interactable currentInteractable;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        XR_HTC_eye_tracker.Interop.GetEyeGazeData(out XrSingleEyeGazeDataHTC[] out_gazes);
        XrSingleEyeGazeDataHTC leftGaze = out_gazes[(int)XrEyePositionHTC.XR_EYE_POSITION_LEFT_HTC];
        XrSingleEyeGazeDataHTC rightGaze = out_gazes[(int)XrEyePositionHTC.XR_EYE_POSITION_RIGHT_HTC];

        if (leftGaze.isValid && rightGaze.isValid) 
        { 
            Vector3 left = leftGaze.gazePose.position.ToUnityVector(); //toUnityVector() -> z -> -z (due to left/right handed differences)
            Vector3 right = rightGaze.gazePose.position.ToUnityVector();
            Vector3 newPos = (left + right) / 2f;
            //Vector3 newPos = right - center;
            transform.localPosition = newPos;

            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.startWidth = 0.02f;
            lineRenderer.endWidth = 0.02f;
            lineRenderer.positionCount = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        XR_HTC_eye_tracker.Interop.GetEyeGazeData(out XrSingleEyeGazeDataHTC[] out_gazes);
        XrSingleEyeGazeDataHTC rightGaze = out_gazes[(int)XrEyePositionHTC.XR_EYE_POSITION_RIGHT_HTC];
        XrSingleEyeGazeDataHTC leftGaze = out_gazes[(int)XrEyePositionHTC.XR_EYE_POSITION_LEFT_HTC];


        if (rightGaze.isValid && leftGaze.isValid)
        {
            Quaternion eyeRotationRight = rightGaze.gazePose.orientation.ToUnityQuaternion();
            Quaternion eyeRotationLeft = leftGaze.gazePose.orientation.ToUnityQuaternion();
            Quaternion eyeRotation = Quaternion.Slerp(eyeRotationLeft, eyeRotationRight, 0.5f);

            transform.rotation = eyeRotation;

            Vector3 gazeDirection = eyeRotation * Vector3.forward;
            Ray ray = new Ray(transform.position, gazeDirection);
            RaycastHit hit;
            Vector3 endPoint = ray.origin + ray.direction * distance;

            lineRenderer.SetPosition(0, ray.origin);
            lineRenderer.SetPosition(1, endPoint);

            /*if (Physics.Raycast(ray, out hit, distance))
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
            }*/

        }
    }
}
