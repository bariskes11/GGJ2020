using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSystem : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collider Çarptı" + collision.gameObject.tag);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Çarptı" + other.tag);
    }

}
