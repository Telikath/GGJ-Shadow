using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    public LevelManager levelManager;
    public bool isStaying = false;
    private bool lastStateIsStaying = false;

    private float maxTimeOutOfLight = 1;
    private float lastTimeOutOfLight = 0;



    void Update()
    {
        if (isStaying == false)
        {
            if (lastStateIsStaying == true)
            {
                lastTimeOutOfLight = Time.time;
            }
            if (Time.time - lastTimeOutOfLight >= maxTimeOutOfLight)
            {
                levelManager.KillPlayer(levelManager.Players[0]);
            }
        }

        lastStateIsStaying = isStaying;
        isStaying = false;
    }
}
