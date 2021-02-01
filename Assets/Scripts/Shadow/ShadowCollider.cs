using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ShadowCollider : MonoBehaviour
{
    public bool showGizmo = true;
    private PolygonCollider2D _myCollider;
    private PolygonCollider2D _myColliderClone;
    public enum lightTypeEnum { Point, Spotlight, Miroir };

    public lightTypeEnum lightType;

    public List<Vector2> newVerticies = new List<Vector2>();

    [Range(5f, 180f)]
    public float angle = 30.0f;
    [Range(1f, 100f)]
    public float rayRange = 100.0f;
    public float coneDirection = 180;
    [Range(2, 100)]
    public int numberOfSegments = 6;
    public LayerMask CastableShadow;
    public LayerMask Miroir;
    public LayerMask Border;
    public LayerMask Lamp;


    void Awake()
    {
        _myCollider = GetComponent<PolygonCollider2D>();
        _myColliderClone = gameObject.AddComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;
        _myColliderClone.isTrigger = true;
    }

    void Update()
    {
        CastPhysicalShadows();
        _myColliderClone.points = _myCollider.points;
    }

    void CastPhysicalShadows()
    {
        switch (lightType)
        {
            case lightTypeEnum.Spotlight:
                {
                    newVerticies = new List<Vector2>();

                    newVerticies.Add(new Vector2(0, 0));


                    float halfFOV = angle / 2.0f;
                    List<Vector3> rays = new List<Vector3>();
                    for (float i = -halfFOV; i <= halfFOV; i += halfFOV / numberOfSegments)
                    {
                        Quaternion rayRotation = Quaternion.AngleAxis(i + coneDirection, Vector3.forward);
                        rays.Add(rayRotation * transform.right * rayRange);
                    }

                    foreach (Vector3 ray in rays)
                    {
                        RaycastHit2D hit = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), ray, rayRange, CastableShadow);
                        if (hit.collider != null)
                        {
                            Vector2 lPos = transform.InverseTransformPoint(hit.point);
                            newVerticies.Add(lPos);
                            if (hit.collider.gameObject.tag == "Miroir")
                            {
                                hit.collider.transform.gameObject.GetComponentInParent<MiroirBehaviour>().HitByRay();
                            }
                        }
                        else
                        {
                            Vector2 lPos = transform.InverseTransformDirection(new Vector2(ray.x, ray.y));
                            newVerticies.Add(lPos);
                        }
                    }

                    _myCollider.points = newVerticies.ToArray();
                    break;
                }

            case lightTypeEnum.Miroir:
                {
                    if (GetComponent<Light2D>().enabled == false)
                    {
                        return;
                    }
                    else
                    {
                        newVerticies = new List<Vector2>();

                        newVerticies.Add(new Vector2(0, 0));


                        float halfFOV = angle / 2.0f;
                        List<Vector3> rays = new List<Vector3>();
                        for (float i = -halfFOV; i <= halfFOV; i += halfFOV / numberOfSegments)
                        {
                            Quaternion rayRotation = Quaternion.AngleAxis(i + coneDirection, Vector3.forward);
                            rays.Add(rayRotation * transform.right * rayRange);
                        }

                        foreach (Vector3 ray in rays)
                        {
                            RaycastHit2D[] hit = Physics2D.RaycastAll(new Vector3(transform.position.x, transform.position.y, transform.position.z), ray, rayRange, CastableShadow);
                            foreach (RaycastHit2D h in hit)
                            {
                                if (h.collider != null)
                                {
                                    if (h.collider.gameObject.tag == "Miroir")
                                    {
                                        if (h.collider.transform.parent.gameObject != transform.gameObject &&
                                        h.collider.transform.gameObject != transform.gameObject)
                                        {
                                            h.collider.transform.gameObject.GetComponentInParent<MiroirBehaviour>().HitByRay();
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        Vector2 lPos = transform.InverseTransformPoint(h.point);
                                        newVerticies.Add(lPos);
                                        break;
                                    }
                                }
                                else
                                {
                                    newVerticies.Add(new Vector2(ray.x, ray.y));
                                }
                            }
                        }

                        _myCollider.points = newVerticies.ToArray();
                    }
                    break;
                }

            case lightTypeEnum.Point:
                {
                    newVerticies = new List<Vector2>();
                    List<Vector3> rays = new List<Vector3>();
                    for (float i = 0; i <= 360; i += 360 / numberOfSegments)
                    {
                        Quaternion rayRotation = Quaternion.AngleAxis(i + coneDirection, Vector3.forward);
                        rays.Add(rayRotation * transform.right * rayRange);
                    }

                    foreach (Vector3 ray in rays)
                    {
                        RaycastHit2D hit = Physics2D.Raycast(transform.position, ray, rayRange, CastableShadow);
                        if (hit.collider != null)
                        {
                            Vector2 lPos = transform.InverseTransformPoint(hit.point);
                            newVerticies.Add(lPos);
                        }
                        else
                        {
                            newVerticies.Add(new Vector2(ray.x, ray.y));
                        }
                    }

                    _myCollider.points = newVerticies.ToArray();
                    break;
                }
        }



    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject.Find("LightTriggerManager").GetComponent<LightTrigger>().isStaying = true;
        }
    }


    void OnDrawGizmos()
    {
        if (showGizmo)
        {
            switch (lightType)
            {
                case lightTypeEnum.Spotlight:
                    {
                        Gizmos.color = Color.yellow;
                        float halfFOV = angle / 2.0f;

                        List<Vector3> rays = new List<Vector3>();
                        for (float i = -halfFOV; i <= halfFOV; i += halfFOV / numberOfSegments)
                        {
                            Quaternion rayRotation = Quaternion.AngleAxis(i + coneDirection, Vector3.forward);
                            rays.Add(rayRotation * transform.right * rayRange);
                        }

                        foreach (Vector3 ray in rays)
                        {
                            Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), ray);
                        }
                        break;
                    }

                case lightTypeEnum.Miroir:
                    {
                        Gizmos.color = Color.yellow;
                        float halfFOV = angle / 2.0f;

                        List<Vector3> rays = new List<Vector3>();
                        for (float i = -halfFOV; i <= halfFOV; i += halfFOV / numberOfSegments)
                        {
                            Quaternion rayRotation = Quaternion.AngleAxis(i + coneDirection, Vector3.forward);
                            rays.Add(rayRotation * transform.right * rayRange);
                        }

                        foreach (Vector3 ray in rays)
                        {
                            Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), ray);
                        }
                        break;
                    }

                case lightTypeEnum.Point:
                    {
                        Gizmos.color = Color.yellow;
                        List<Vector3> rays = new List<Vector3>();
                        for (float i = 0; i <= 360; i += 360 / numberOfSegments)
                        {
                            Quaternion rayRotation = Quaternion.AngleAxis(i + coneDirection, Vector3.forward);
                            rays.Add(rayRotation * transform.right * rayRange);
                        }

                        foreach (Vector3 ray in rays)
                        {
                            Gizmos.DrawRay(transform.position, ray);
                        }
                        break;
                    }
            }
        }

    }
}
