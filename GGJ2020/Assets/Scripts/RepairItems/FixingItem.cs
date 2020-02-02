using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class FixingItem : MonoBehaviour
{
    // Start is called before the first frame update

    public float DestructionTimeOut=120F;
    private HeadBob ply;
    void Start()
    {
        ply = GameObject.FindObjectOfType<HeadBob>();
        if (ply != null)
        {
            ply.ShakeCamera(2F, 0.001F);
        }
        StartCoroutine(DestroyThisObject());
    }

    IEnumerator DestroyThisObject()
    {
        yield return new WaitForSeconds(DestructionTimeOut);
        GameObject.Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
