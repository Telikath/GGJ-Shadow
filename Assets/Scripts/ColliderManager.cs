using MoreMountains.CorgiEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    public GameObject triggerComposite;

    public LevelManager levelManager;

    void Awake()
    {            
        if (triggerComposite.GetComponent<LightTrigger>().levelManager == null)
        {
            try
            {
                triggerComposite.GetComponent<LightTrigger>().levelManager = levelManager;
            }
            catch
            {

            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (triggerComposite.GetComponent<LightTrigger>().levelManager == null)
        {
            try
            {
                triggerComposite.GetComponent<LightTrigger>().levelManager = levelManager;
            }
            catch
            {

            }
        }
    }
}
