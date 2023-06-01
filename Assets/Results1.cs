using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Results1 : MonoBehaviour
{
    public List<float> DeviationValues = new List<float>(); 

    public ScubaDive1 sd1;

    public float timeSinceLastUpdate = 0.0f; // a variable to track the time since the last update

    public void Start()
    {
        sd1 = GetComponent<ScubaDive1>();
    }

    public void FixedUpdate()
    {
        if (sd1.gameStarted)
        {
            timeSinceLastUpdate += Time.fixedDeltaTime;

            if (timeSinceLastUpdate >= 0.5f)
            {
                float currentDeviation = 10.0f- sd1.depth;
                DeviationValues.Add(currentDeviation);
                timeSinceLastUpdate = 0.0f;
            }
        }
    }
}
