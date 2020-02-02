using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class FixingItem : MonoBehaviour
{
    // Start is called before the first frame update


    private HeadBob ply;
    void Start()
    {
        ply = GameObject.FindObjectOfType<HeadBob>();
        if (ply != null)
        {
            ply.ShakeCamera(5F, 0.01F);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
