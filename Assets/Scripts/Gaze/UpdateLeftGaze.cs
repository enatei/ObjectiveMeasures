using UnityEngine;
using VIVE.OpenXR;
using VIVE.OpenXR.EyeTracker;


public class UpdateLeftGaze : MonoBehaviour
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
        float gazeDistance = 0.20f;

        if (leftGaze.isValid)
        {
            // Blickrichtung aus der Gaze-Rotation berechnen
            Quaternion gazeRotation = leftGaze.gazePose.orientation.ToUnityQuaternion();
            Vector3 gazeDirection = Camera.transform.rotation * (gazeRotation * Vector3.forward);

            // Basisposition: fester Punkt vor der Kamera
            Vector3 basePoint = Camera.transform.position + Camera.transform.forward * gazeDistance;

            // Zielposition: basePoint verschoben in Blickrichtung
            transform.position = basePoint + gazeDirection;
        }
    }
}
