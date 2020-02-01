using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBounds : MonoBehaviour
{
    public float maxX;
    public float maxY;
    private BoxCollider bCollider;

    private void Awake()
    {
        bCollider = this.GetComponent<BoxCollider>();
       Vector3 maxVTR=  bCollider.bounds.max;
        Vector3 minVTR = bCollider.bounds.min;
        print(maxVTR);
        print(minVTR);


    }
}
