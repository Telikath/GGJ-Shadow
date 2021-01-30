using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class MiroirBehaviour : MonoBehaviour
{
    public int compteurRays = 0;
    [Range (1,10)]
    public int nombreRequisRays = 3;

    public bool miroirState = false;


    void LateUpdate()
    {
        if (compteurRays < nombreRequisRays)
        {
            GetComponent<Light2D>().enabled = false;
            GetComponent<PolygonCollider2D>().enabled = false;
        }

        compteurRays = 0;
        miroirState = false;
    }

    void Awake()
    {
        GetComponent<Light2D>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;
    }

    public void HitByRay ()
    {
        compteurRays++;
        if (compteurRays >= nombreRequisRays)
        {
            miroirState = true;
            GetComponent<Light2D>().enabled = true;
            GetComponent<PolygonCollider2D>().enabled = true;
        }
        else
        {
            GetComponent<Light2D>().enabled = false;
            GetComponent<PolygonCollider2D>().enabled = false;
        }
    }
}
