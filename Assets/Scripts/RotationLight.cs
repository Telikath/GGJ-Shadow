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
    

    [Range(0,359)]
    public int maxAngle = 100;

    [Range(0, 359)]
    public int minAngle = 0;

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
            if (transform.rotation.eulerAngles.z % 360 >= maxAngle)
            {
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, maxAngle);
            }
            else if (transform.rotation.eulerAngles.z % 360 <= minAngle)
            {
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, minAngle);
            }
        }

    }

    void CheckClickOnLight()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics2D.Raycast(mousePos2D, Vector2.zero).collider == GetComponent<CircleCollider2D>()
                || Physics2D.Raycast(mousePos2D, Vector2.zero).collider == arrowCollider)
            {
                stateRotation = true;
            }
            else
            {
                stateRotation = false;
            }
            arrowCollider.gameObject.SetActive(stateRotation);
        }

    }

    void RotateLight()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (Input.GetMouseButton(0) && Physics2D.Raycast(mousePos2D, Vector2.zero).collider == arrowCollider)
        {
            Vector2 diff = hit.point - new Vector2(transform.position.x, transform.position.y);
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, (rot_z ));
        }
    }

    void OnDrawGizmos()
    {
        if (showGizmo)
        {
            Gizmos.color = Color.yellow;
            Quaternion rayRotation = Quaternion.AngleAxis(minAngle, Vector3.forward);
            Gizmos.DrawRay(transform.position, rayRotation * transform.right * gizmoLineSize);

            rayRotation = Quaternion.AngleAxis(maxAngle, Vector3.forward);
            Gizmos.DrawRay(transform.position, rayRotation * transform.right * gizmoLineSize);

            Gizmos.color = Color.yellow;
            for (int i = minAngle; i <= maxAngle; i += 2)
            {
                rayRotation = Quaternion.AngleAxis(i, Vector3.forward);
                Gizmos.DrawRay(transform.position, rayRotation * transform.right * gizmoLineSize);
            }
        }
    }


}
