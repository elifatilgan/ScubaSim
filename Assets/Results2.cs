using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Results2 : MonoBehaviour
{
    public List<float> DeviationValues = new List<float>(); // a list to store the values of x

    public ScubaDive2 sd2;

    public float timeSinceLastUpdate = 0.0f; // a variable to track the time since the last update

    public void Start()
    {
        sd2 = GetComponent<ScubaDive2>();
    }

    public void FixedUpdate()
    {
        if(sd2.gameStarted) { 
        timeSinceLastUpdate += Time.fixedDeltaTime;

        if (timeSinceLastUpdate >= 0.5f)
        {
            float currentDeviation = 10.0f-sd2.depth;
            DeviationValues.Add(currentDeviation);
            timeSinceLastUpdate = 0.0f;
        }
        }
    }
}