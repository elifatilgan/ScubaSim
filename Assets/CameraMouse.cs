using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouse : MonoBehaviour
{
    public float sensitivity = 5.0f;
    public float minY = -60.0f;
    public float maxY = 60.0f;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    void Start()
    {
        // Lock cursor to the game window
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate the camera based on mouse movement
        rotationX += mouseX * sensitivity;
        rotationY += mouseY * sensitivity;
        rotationY = Mathf.Clamp(rotationY, minY, maxY);

        transform.localRotation = Quaternion.Euler(-rotationY, rotationX, 0.0f);
    }
}

