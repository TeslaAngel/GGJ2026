using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RenderOrderSorter : MonoBehaviour
{
    public int orderOffset = 0;     // fine tune per object
    public float yMultiplier = 100f; // precision (100 = 0.01 units)

    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        // Lower Y should be drawn on top
        sr.sortingOrder = (int)(-transform.position.y * yMultiplier) + orderOffset;
    }
}
