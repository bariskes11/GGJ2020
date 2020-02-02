using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmRotator : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10F;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,(speed * Time.deltaTime),Space.Self);
    }
}
