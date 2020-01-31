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
        
        waterObject.transform.Translate(Vector3.up*Time.deltaTime*floatSpeed*OpenedDorCount*UnRepairedFieldCount);
    }





}
