using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouse2 : MonoBehaviour
{
    public float sensitivity = 100f;

    float xRotation = 0f;
    float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseX;
        yRotation %= 360f;

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}






