using UnityEngine;
using VIVE.OpenXR;
using VIVE.OpenXR.EyeTracker;


public class LeftGaze : MonoBehaviour
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

        if (leftGaze.isValid)
        {
            Vector3 left = leftGaze.gazePose.position.ToUnityVector();
            Vector3 right = rightGaze.gazePose.position.ToUnityVector();
            Vector3 center = (left + right) / 2f;
            Vector3 newPos = left - center;
            transform.localPosition = newPos;


        }

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.positionCount = 2;

    }

    // Update is called once per frame
    void Update()
    {
        XR_HTC_eye_tracker.Interop.GetEyeGazeData(out XrSingleEyeGazeDataHTC[] out_gazes);
        XrSingleEyeGazeDataHTC leftGaze = out_gazes[(int)XrEyePositionHTC.XR_EYE_POSITION_LEFT_HTC];

        if (leftGaze.isValid)
        {
            Quaternion eyeRotation = leftGaze.gazePose.orientation.ToUnityQuaternion();
            Vector3 cameraPos = camera.transform.position;
            Vector3 eyePos = transform.position;
            Vector3 direction = eyePos - cameraPos;



            XrQuaternionf eyeRotationOriginal = leftGaze.gazePose.orientation;
            Quaternion correction = Quaternion.Euler(0, 0, 0);
            Quaternion fineTune = Quaternion.Euler(0, 0, 0);


            transform.rotation = fineTune * correction * eyeRotation;

            Vector3 gazeDirection = (fineTune * correction * eyeRotation) * Vector3.forward;
            Ray ray = new Ray(transform.position, gazeDirection);
            Vector3 endPoint = ray.origin + ray.direction * distance;

            lineRenderer.SetPosition(0, ray.origin);
            lineRenderer.SetPosition(1, endPoint);
        }
    }
}