using UnityEngine;
using VIVE.OpenXR;
using VIVE.OpenXR.EyeTracker;


public class LeftGaze : MonoBehaviour
{

    public Camera Camera;



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
            //newPos.y = 0.3f;
            transform.localPosition = newPos;

        }

    }

    // Update is called once per frame
    void Update()
    {
        XR_HTC_eye_tracker.Interop.GetEyeGazeData(out XrSingleEyeGazeDataHTC[] out_gazes);
        XrSingleEyeGazeDataHTC leftGaze = out_gazes[(int)XrEyePositionHTC.XR_EYE_POSITION_LEFT_HTC];
        XrSingleEyeGazeDataHTC rightGaze = out_gazes[(int)XrEyePositionHTC.XR_EYE_POSITION_RIGHT_HTC];

        if (leftGaze.isValid)
        {
            Quaternion eyeRotation = leftGaze.gazePose.orientation.ToUnityQuaternion();
            Quaternion correction = Quaternion.Euler(0, 180, 0);
            Quaternion fineTune = Quaternion.Euler(0, -52.5f, 0);

            transform.rotation = fineTune * correction * eyeRotation;
        }
    }
}