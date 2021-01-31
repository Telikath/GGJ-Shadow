using System;
using UnityEngine;


public class PositionControl : MonoBehaviour
{
    public GameObject myLight;

    [Range(-180f, 180f)]
    public float angleMax;
    [Range(-180f, 180f)]
    public float angleMin;
    void Update()
    {
        /*if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), mousePosition);

            if (Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), mousePosition).collider == GetComponent<CircleCollider2D>())
            {
                if (hit.collider != null)
                {
                    Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
                    Vector3 dir = Input.mousePosition - pos;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    
                    if (angle > angleMax) 
                    {
                        angle = angleMax;
                    }
                    else if (angle < angleMin)
                    {
                        angle = angleMin;
                    }
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    myLight.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
            }

        }*/

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), mousePosition);

            if (Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), mousePosition).collider == GetComponent<CircleCollider2D>())
            {

            }

                //transform.position = (transform.position - otherObject.transform.position).normalized * distance + otherObject.transform.position;
        }



    }

}

