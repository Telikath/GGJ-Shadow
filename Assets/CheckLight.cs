using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;

public class CheckLight : MonoBehaviour
{
    private CircleCollider2D trigger;
    public float timeBeforeDie = 2;
    private float initiateKillTime = 0;
    private bool initiateKill = false;


    void Update()
    {
        if (initiateKill == true)
        {
            if (Time.time + timeBeforeDie >= initiateKillTime)
            {
                initiateKill = false;
                GameObject.Find("LevelManager").GetComponent<LevelManager>().KillPlayer(GetComponent<Character>());
            }
        }
    }

    void LateUpdate()
    {
        if (initiateKill != true)
        {
            initiateKillTime = Time.time;
        }
        initiateKill = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        Debug.Log("InLight");
        initiateKill = false;
    }
}
