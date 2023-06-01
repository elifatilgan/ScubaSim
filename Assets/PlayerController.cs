using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera camera;
    public float speed = 10f;

    private void Update()
    {
        // Get the camera's forward and right vectors
        Vector3 cameraForward = camera.transform.forward;
        Vector3 cameraRight = camera.transform.right;

        // Set the character's forward direction to match the camera's forward direction
        transform.forward = new Vector3(cameraForward.x, 0, cameraForward.z);

        // Handle movement controls
       
        

        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) // Move forward
        {
            direction += transform.forward;
        }
        else if (Input.GetKey(KeyCode.S)) // Move backward
        {
            direction -= transform.forward;
        }

        if (Input.GetKey(KeyCode.D)) // Move right
        {
            direction += cameraRight;
        }
        else if (Input.GetKey(KeyCode.A)) // Move left
        {
            direction -= cameraRight;
        }

        if (Input.GetKey(KeyCode.K)) // Move up
        {
            direction += Vector3.up;
        }
        if (Input.GetKey(KeyCode.J)) // Move down
        {
            direction -= Vector3.up;
        }

        // Normalize the direction vector to ensure that the character doesn't move faster diagonally
        if (direction.magnitude > 1f)
        {
            direction.Normalize();
        }

        // Move the character in the desired direction
        transform.position += direction * speed * Time.deltaTime;
    }
}