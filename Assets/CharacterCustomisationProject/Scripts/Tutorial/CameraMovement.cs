using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public enum RotationalAxis
    {
        MouseX,
        MouseY
    }

    public RotationalAxis axis = RotationalAxis.MouseX;
    public float sensitivity = 15.0f;
    public float minY = -60.0f, maxY = 60.0f;
    private float rotY;
    
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        if(GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (axis == RotationalAxis.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime, 0);
        }

        else
        {
            rotY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
            rotY = Mathf.Clamp(rotY, minY, maxY);
            transform.localEulerAngles = new Vector3(-rotY, 0, 0);
        }
    }
}
