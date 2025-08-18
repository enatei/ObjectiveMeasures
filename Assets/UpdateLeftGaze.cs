using UnityEngine;
using VIVE.OpenXR;
using VIVE.OpenXR.EyeTracker;


public class UpdateLeftGaze : MonoBehaviour
{
    public Transform xrCamera; 
    public float distanceInMeters = 0.05f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        XR_HTC_eye_tracker.Interop.GetEyeGazeData(out XrSingleEyeGazeDataHTC[] out_gazes);
        XrSingleEyeGazeDataHTC leftGaze = out_gazes[(int)XrEyePositionHTC.XR_EYE_POSITION_LEFT_HTC];

        if (leftGaze.isValid)
        {
            Quaternion originalRotation = leftGaze.gazePose.orientation.ToUnityQuaternion();
            Vector3 gazeDirection = originalRotation * Vector3.forward;
            gazeDirection.y *= -1;

            Quaternion fixedRotation = Quaternion.LookRotation(gazeDirection, Vector3.up);

            Vector3 localEyePos = leftGaze.gazePose.position.ToUnityVector();
            Vector3 worldEyePos = xrCamera != null ? xrCamera.TransformPoint(localEyePos) : localEyePos;

            Vector3 targetPosition = worldEyePos + gazeDirection.normalized * distanceInMeters;

            transform.position = targetPosition;
            transform.rotation = fixedRotation;
        }
    }
}
