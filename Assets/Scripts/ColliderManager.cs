using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    private CompositeCollider2D composite;

    void Awake()
    {
        composite = GetComponent<CompositeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        composite.GenerateGeometry();
    }
}
