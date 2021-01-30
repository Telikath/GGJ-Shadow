using UnityEngine;


public class rotate : MonoBehaviour
{

    public GameObject myLight;
    public float moveSpeed = 1;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), mousePosition);

            if (Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), mousePosition).collider == GetComponent<CircleCollider2D>())
            {
                if (hit.collider != null)
                {
                    // position de la cible
                    Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - hit.collider.gameObject.transform.position;
                    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                    hit.collider.gameObject.transform.position = mousePosition;

                    // rotaion de la light
                    /*Vector3 difference = hit.collider.gameObject.transform.position - myLight.transform.position;
                    float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                    myLight.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);*/

                    // 

                }
            }

        }

    }
}

