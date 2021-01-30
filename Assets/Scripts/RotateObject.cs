using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{

    public float RotationSpeed = 1;


    private Quaternion _lookRotation;
    private Vector3 _direction;

    void Start()
    {
    }

    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), mousePosition);

            if (hit.collider != null && hit.collider.gameObject.tag == "Rotative")
            {
                Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - hit.collider.gameObject.transform.position;
                diff.Normalize();

                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                hit.collider.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, (rot_z - 90));
            }


        }
    }
}
