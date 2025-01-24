using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 1f;
    public float scrollSensitivity = 5f;

    public float centerOffset = 3f;
    public float verticalOffset = 2f;

    public float minScroll = 4f;
    public float maxScroll = 15f;

    public Transform cameraTransform;

    private float elevation = 0f;
    private float heading = 0f;
    private float distance = 10f;

    private Vector2 motion = Vector2.zero;
    public float motionFallof = 0.1f;

    [Range(0, 90)] public float minElevation = 0f;

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            motion.y -= Input.GetAxis("Mouse Y") * sensitivity;
            motion.x += Input.GetAxis("Mouse X") * sensitivity;
        }

        distance += Input.mouseScrollDelta.y;
        distance = Mathf.Clamp(distance, minScroll, maxScroll);

        elevation += motion.y;
        heading += motion.x;

        elevation = Mathf.Clamp(elevation, minElevation, 89.9f);
        heading = Mathf.Repeat(heading, 360);

        transform.rotation = Quaternion.AngleAxis(heading, Vector3.up) * Quaternion.AngleAxis(elevation, Vector3.right);

        float angleMapped = 1 - Mathf.InverseLerp(minElevation, 89.9f, elevation);

        transform.position = Vector3.ProjectOnPlane(-transform.forward, Vector3.up).normalized * (angleMapped * centerOffset);

        cameraTransform.localPosition = -Vector3.forward * distance;

        motion = Vector2.Lerp(motion, Vector2.zero, motionFallof);
    }
}
