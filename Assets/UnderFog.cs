using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnderFog : MonoBehaviour
   
{
    [SerializeField] private Gradient _gradient;
    [SerializeField] private float _fogStartDistance = 5f;
    [SerializeField] private float _fogEndDistance = 20f;

    private void Update()
    {
        // Get the camera's current depth
        float depth = transform.position.y;

        // Evaluate the gradient at the current depth to get the fog color
        Color color = _gradient.Evaluate(depth);

        // Set the fog color and distance based on the camera's depth
        RenderSettings.fogColor = color;
        RenderSettings.fogStartDistance = _fogStartDistance + depth;
        RenderSettings.fogEndDistance = _fogEndDistance + depth;
    }
}


