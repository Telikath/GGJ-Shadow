using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopControl : MonoBehaviour
{

    public GameObject control;
    private bool set = false;
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        if (Input.GetMouseButtonUp(0) && Physics2D.Raycast(mousePos2D, Vector2.zero).collider == GetComponent<Collider2D>())
        {
            if (!set)
            {
                set = true;
                control.SetActive(true);
            }
            else
            {
                set = false;
                control.SetActive(false);
            }

        }

        if (set)
        {
            Vector3 difference = control.transform.position - transform.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        }
        
    }
    
}
