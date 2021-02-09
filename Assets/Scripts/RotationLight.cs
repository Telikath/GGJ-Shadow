using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationLight : MonoBehaviour
{
    private CircleCollider2D arrowCollider;
    private bool stateRotation;
    private int gizmoLineSize = 2;
    public bool canBeRotate = true;


    [Range(25,335)]
    public int maxAngle = 335;

    [Range(25, 335)]
    public int minAngle = 25;

    public bool showGizmo = true;

    void Awake()
    {
        arrowCollider = GameObject.Find(gameObject.name.ToString() + "Arrow").GetComponent<CircleCollider2D>();
        stateRotation = false;
        arrowCollider.gameObject.SetActive(false);
    }


    void Update()
    {
        if (canBeRotate)
        {
            CheckClickOnLight();
            if (stateRotation)
            {
                RotateLight();
            }


        }

    }

    void CheckClickOnLight()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        if (Input.GetMouseButtonDown(0) && !stateRotation)
        {
            if (Physics2D.Raycast(mousePos2D, Vector2.zero).collider == GetComponent<CircleCollider2D>())
            {
                stateRotation = true;
            }
        }
        else if(Input.GetMouseButtonDown(0) && stateRotation)
        {
            stateRotation = false;
        }

        arrowCollider.gameObject.SetActive(stateRotation);
    }

    void RotateLight()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        Vector2 diff = hit.point - new Vector2(transform.position.x, transform.position.y);
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        Vector3 finalRot = Quaternion.Euler(0f, 0f, rot_z).eulerAngles;

        if(finalRot.z < maxAngle && finalRot.z > minAngle)
        {
          transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        }

    }

    void OnDrawGizmos()
    {
        if (showGizmo)
        {
            Gizmos.color = Color.yellow;
            Quaternion rayRotation = Quaternion.AngleAxis(minAngle - transform.rotation.eulerAngles.z, Vector3.forward);
            Gizmos.DrawRay(transform.position, rayRotation * transform.right * gizmoLineSize);

            rayRotation = Quaternion.AngleAxis(maxAngle - transform.rotation.eulerAngles.z, Vector3.forward);
            Gizmos.DrawRay(transform.position, rayRotation * transform.right * gizmoLineSize);

            Gizmos.color = Color.yellow;
            for (int i = minAngle; i <= maxAngle; i += 2)
            {
                rayRotation = Quaternion.AngleAxis(i - transform.rotation.eulerAngles.z, Vector3.forward);
                Gizmos.DrawRay(transform.position, rayRotation * transform.right * gizmoLineSize);
            }
        }
    }


}
