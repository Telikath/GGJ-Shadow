using MoreMountains.CorgiEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    public GameObject triggerComposite;
    private CompositeCollider2D composite;
    private Character player;
    private int outCount = 0;

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
        
        composite = GetComponent<CompositeCollider2D>();
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
