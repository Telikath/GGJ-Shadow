using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    public LevelManager levelManager;
    public bool isStaying = false;



    void Update()
    {
        if (isStaying == false)
        {
            levelManager.KillPlayer(levelManager.Players[0]);
        }
        isStaying = false;
    }
}
