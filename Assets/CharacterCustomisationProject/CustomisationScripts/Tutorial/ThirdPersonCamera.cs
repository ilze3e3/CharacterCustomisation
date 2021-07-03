using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ThirdPersonCamera : MonoBehaviour
{
    #region Camera Control Vars
    [SerializeField] float mouseSensivity = 10;
    [SerializeField] Transform target;
    [SerializeField] float distFromTarget = 2;
    [SerializeField] Vector2 pitchMinMax = new Vector2(-40, 85);
    #endregion

    #region Camera Smoothing Vars
    [SerializeField] float rotationSmoothTime = .12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;
    #endregion

    float yaw;
    float pitch;

    private void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensivity;
            pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
        }

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;
        transform.position = target.position - transform.forward * distFromTarget;
    }
}