using UnityEngine;
using VIVE.OpenXR;
using VIVE.OpenXR.EyeTracker;


public class globalUpdateGaze : MonoBehaviour
{

    public Camera Camera;



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
            Quaternion eyeRotation = leftGaze.gazePose.orientation.ToUnityQuaternion();
            Quaternion correction = Quaternion.Euler(0, 180, 0);
            Quaternion fineTune = Quaternion.Euler(0, -45, 0);

            transform.rotation =fineTune * correction * eyeRotation;

        }
    }
}