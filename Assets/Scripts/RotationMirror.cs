using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationMirror : MonoBehaviour
{
  private SpriteRenderer arrowSpriteRenderer;
  private CircleCollider2D arrowCollider;
  public float speed = 10f;
  public Transform target;
  private bool stateRotation;
  public bool canBeRotated = true;
  private int gizmoLineSize = 2;

  [Range(25,335)]
  public int maxAngle = 335;

  [Range(25, 335)]
  public int minAngle = 25;

  public bool showGizmo = true;

  Color angleColor = Color.green;

  void Awake()
  {
      arrowCollider = GameObject.Find(gameObject.name.ToString() + "Arrow").GetComponent<CircleCollider2D>();
      arrowSpriteRenderer = GameObject.Find(gameObject.name.ToString() + "Arrow").GetComponent<SpriteRenderer>();
      stateRotation = false;
      arrowCollider.gameObject.SetActive(false);

  }

    void Update()
    {
      if (canBeRotated)
      {
          CheckClick();
          if (stateRotation)
          {
            RotateObj();
          }

          if (transform.rotation.eulerAngles.z >= maxAngle)
          {
              transform.rotation = Quaternion.Euler(0.0f, 0.0f, maxAngle);
          }
          else if (transform.rotation.eulerAngles.z <= minAngle)
          {
              transform.rotation = Quaternion.Euler(0.0f, 0.0f, minAngle);
          }
      }

    }

    void CheckClick()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics2D.Raycast(mousePos2D, Vector2.zero).collider == GetComponent<CircleCollider2D>())
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

    void RotateObj()
    {

      Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
      //Vector2 mousePos = target.position - transform.position;
      float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
      arrowSpriteRenderer.color = Color.Lerp(Color.red,Color.green,Mathf.InverseLerp(minAngle,maxAngle,transform.rotation.eulerAngles.z));
      Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
      transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
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
