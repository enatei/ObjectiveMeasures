using UnityEngine;
using VIVE.OpenXR.EyeTracker;
using VIVE.OpenXR;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class RightGaze : MonoBehaviour
{
    public Camera camera;
    public float distance;
    LineRenderer lineRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        XR_HTC_eye_tracker.Interop.GetEyeGazeData(out XrSingleEyeGazeDataHTC[] out_gazes);
        XrSingleEyeGazeDataHTC leftGaze = out_gazes[(int)XrEyePositionHTC.XR_EYE_POSITION_LEFT_HTC];
        XrSingleEyeGazeDataHTC rightGaze = out_gazes[(int)XrEyePositionHTC.XR_EYE_POSITION_RIGHT_HTC];

        if (rightGaze.isValid)
        {

            //Xr coordinate system: righthanded
            //Unity coordinate system: lefthanded
            Vector3 left = leftGaze.gazePose.position.ToUnityVector(); //toUnityVector() -> z -> -z (due to left/right handed differences)
            Vector3 right = rightGaze.gazePose.position.ToUnityVector();
            Vector3 center = (left + right) / 2f;
            Vector3 newPos = right - center;
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


        if (rightGaze.isValid)
        {
            Quaternion eyeRotation = rightGaze.gazePose.orientation.ToUnityQuaternion();
            transform.rotation = eyeRotation;

            Vector3 gazeDirection = eyeRotation * Vector3.forward;
            Ray ray = new Ray(transform.position, gazeDirection);
            Vector3 endPoint = ray.origin + ray.direction * distance;

            lineRenderer.SetPosition(0, ray.origin);
            lineRenderer.SetPosition(1, endPoint);

        }
    }
}
