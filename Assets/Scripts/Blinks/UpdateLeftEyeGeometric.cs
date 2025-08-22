using UnityEngine;
using VIVE.OpenXR;
using VIVE.OpenXR.EyeTracker;

public class UpdateLeftEyeGeometric : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        XR_HTC_eye_tracker.Interop.GetEyeGeometricData(out XrSingleEyeGeometricDataHTC[] out_geometrics);
        XrSingleEyeGeometricDataHTC leftGeometric = out_geometrics[(int)XrEyePositionHTC.XR_EYE_POSITION_LEFT_HTC];
        if (leftGeometric.isValid)
        {
            float leftEyeOpenness = leftGeometric.eyeOpenness;
            //float rightEyeeyeSqueeze = rightGeometric.eyeSqueeze;
            //float rightEyeeyeWide = rightGeometric.eyeWide;

            Vector3 scale = transform.localScale;
            scale.y = leftEyeOpenness;
            transform.localScale = scale;
        }
    }
}
