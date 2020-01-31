using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSystem : MonoBehaviour
{
    [SerializeField]
    GameObject waterObject;

    public float floatSpeed;
    public int OpenedDorCount;
    public int UnRepairedFieldCount;

    private void Start()
    {
        
    }
    private void Update()
    {

        /// su Y ekseninde yukariya doğru ilerleyecek
        if (OpenedDorCount > 0)
        {
            UnRepairedFieldCount = 1;
        }
        if (UnRepairedFieldCount > 0)
        {
            OpenedDorCount = 1;
        }
        waterObject.transform.Translate(Vector3.up*Time.deltaTime*floatSpeed*OpenedDorCount*UnRepairedFieldCount);
    }





}
