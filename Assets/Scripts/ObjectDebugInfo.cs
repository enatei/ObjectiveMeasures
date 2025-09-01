using UnityEngine;
using VIVE.OpenXR.EyeTracker;
using VIVE.OpenXR;
using VIVE.OpenXR;
using VIVE.OpenXR.EyeTracker;

public class ObjectDebugInfo : MonoBehaviour
{
    public GameObject target;

    [Header("GUI-Stil")]
    public Vector2 offset = new Vector2(10, 10);
    public GUIStyle textStyle;

    private bool leftGazeValid = false;
    private Quaternion leftEyeRot;
    private Vector3 leftEyePos;

    private Vector3 previousEyePos;
    private Quaternion previousEyeRot;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (textStyle == null)
        {
            textStyle = new GUIStyle(GUI.skin.label);
            textStyle.fontSize = 25;
            textStyle.normal.textColor = Color.white;
        }

    }

    // Update is called once per frame
    void Update()
    {
        XR_HTC_eye_tracker.Interop.GetEyeGazeData(out XrSingleEyeGazeDataHTC[] out_gazes);
        XrSingleEyeGazeDataHTC leftGaze = out_gazes[(int)XrEyePositionHTC.XR_EYE_POSITION_LEFT_HTC];

        if (leftGaze.isValid)
        {
            if (leftEyePos != null && leftEyeRot != null)
            {
                previousEyePos = leftEyePos;
                previousEyeRot = leftEyeRot;
            }


            leftGazeValid = true;
            leftEyeRot = leftGaze.gazePose.orientation.ToUnityQuaternion();
            leftEyePos = leftGaze.gazePose.position.ToUnityVector();
        }

    }

    void OnGUI()
    {
        if (target == null) return;

        Vector3 pos = target.transform.position;
        Vector3 rot = target.transform.eulerAngles;

        Vector3 localPos = target.transform.localPosition;
        Vector3 localRot = target.transform.localEulerAngles;

        Vector3 objectForward = target.transform.forward;


        string info = $"globalPosition:\nX: {pos.x:F2}, Y: {pos.y:F2}, Z: {pos.z:F2}\n" +
                      $"globalRotation:\nX: {rot.x:F1}°, Y: {rot.y:F1}°, Z: {rot.z:F1}°\n" +
                      $"localPosition:\nX: {localPos.x:F2}, Y: {localPos.y:F2}, Z: {localPos.z:F2}\n" +
                      $"localRotation:\nX: {localRot.x:F1}°, Y: {localRot.y:F1}°, Z: {localRot.z:F1}\n°" +
                      $"object forward:\nX: {objectForward.x:F2}, Y: {objectForward.y:F2}, Z: {objectForward.z:F2}\n";

        if (leftGazeValid)
        {
            Vector3 cameraRot = transform.eulerAngles;
            Vector3 cameraPos = transform.position;
            Vector3 cameraForward = transform.forward;

            info += $"\nCamera Position:\nX: {cameraPos.x:F2}, Y: {cameraPos.y:F2}, Z: {cameraPos.z:F2}\n" +
                    $"Camera Rotation:\nX: {cameraRot.x:F1}°, Y: {cameraRot.y:F1}°, Z: {cameraRot.z:F1}°\n" +
                    $"\nCamera Forward:\nX: {cameraForward.x:F2}, Y: {cameraForward.y:F2}, Z: {cameraForward.z:F2}\n" +
                    $"New Eye Position:\nX: {leftEyePos.x:F2}, Y: {leftEyePos.y:F2}, Z: {leftEyePos.z:F2}\n" +
                    $"New Eye Rotation:\nX: {leftEyeRot.eulerAngles.x:F1}°, Y: {leftEyeRot.eulerAngles.y:F1}°, Z: {leftEyeRot.eulerAngles.z:F1}°\n";

            if (previousEyeRot != null && previousEyeRot != null)
            {
                info += $"Old Eye Position:\nX: {previousEyePos.x:F2}, Y: {previousEyePos.y:F2}, Z: {previousEyePos.z:F2}\n" +
                 $"Old Eye Rotation:\nX: {previousEyeRot.eulerAngles.x:F1}°, Y: {previousEyeRot.eulerAngles.y:F1}°, Z: {previousEyeRot.eulerAngles.z:F1}°\n";
            }
        }
        else
        {
            info += "\nleftGaze not Valid";
        }

        Vector2 size = textStyle.CalcSize(new GUIContent(info));

        Rect rect = new Rect(
            Screen.width - size.x - offset.x,
            Screen.height - size.y - offset.y,
            size.x,
            size.y
        );

        GUI.Label(rect, info, textStyle);
    }
}
