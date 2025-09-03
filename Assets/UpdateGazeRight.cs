using UnityEngine;
using VIVE.OpenXR.EyeTracker;
using VIVE.OpenXR;

public class UpdateGazeRight : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        XR_HTC_eye_tracker.Interop.GetEyeGazeData(out XrSingleEyeGazeDataHTC[] out_gazes);
        XrSingleEyeGazeDataHTC leftGaze = out_gazes[(int)XrEyePositionHTC.XR_EYE_POSITION_LEFT_HTC];
        XrSingleEyeGazeDataHTC rightGaze = out_gazes[(int)XrEyePositionHTC.XR_EYE_POSITION_RIGHT_HTC];

        if (rightGaze.isValid)
        {
            Vector3 left = leftGaze.gazePose.position.ToUnityVector();
            Vector3 right = rightGaze.gazePose.position.ToUnityVector();
            Vector3 center = (left + right) / 2f;
            transform.localPosition = right - center;

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
            Quaternion correction = Quaternion.Euler(0, 180, 0);
            Quaternion fineTune = Quaternion.Euler(0, -45, 0);
            //Quaternion fineTune = Quaternion.Euler(0, -55, 0);


            transform.rotation = fineTune * correction * eyeRotation;

        }
    }
}
