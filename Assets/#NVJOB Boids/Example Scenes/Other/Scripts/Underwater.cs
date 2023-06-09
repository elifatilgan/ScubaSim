﻿using UnityEngine;

[AddComponentMenu("#NVJOB/Tools/Underwater")]


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


public class Underwater : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    

    public float waterLevel = -27;
    public GameObject underwater;
    public Renderer horizenDown;
    public Material horizenDownMat1, horizenDownMat2;

    //--------------

    Transform thisTransform;


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void Awake()
    {
        //--------------

        thisTransform = transform;
        underwater.SetActive(false);
        horizenDown.material = horizenDownMat1;

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void LateUpdate()
    {
        //--------------

        if (thisTransform.position.y < waterLevel)
        {
            if (!underwater.activeSelf)
            {
                underwater.SetActive(true);
                horizenDown.material = horizenDownMat2;
            }
        }
        else
        {
            if (underwater.activeSelf)
            {
                underwater.SetActive(false);
                horizenDown.material = horizenDownMat1;
            }
        }

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}