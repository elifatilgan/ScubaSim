using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Results3 : MonoBehaviour
{
    public List<float> DeviationValues = new List<float>(); 

    public ScubaDive3 sd3;

    public float timeSinceLastUpdate = 0.0f; // a variable to track the time since the last update

    public void Start()
    {
        sd3 = GetComponent<ScubaDive3>();
    }

    public void FixedUpdate()
    {
        if (sd3.gameStarted)
        {
            timeSinceLastUpdate += Time.fixedDeltaTime;

            if (timeSinceLastUpdate >= 0.5f)
            {
                float currentDeviation = 10.0f- sd3.depth;
                DeviationValues.Add(currentDeviation);
                timeSinceLastUpdate = 0.0f;
            }
        }
    }
}
