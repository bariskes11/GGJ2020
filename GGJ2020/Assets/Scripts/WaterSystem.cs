using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaterSystem : MonoBehaviour
{
    [SerializeField]
    GameObject waterObject;

    public float floatSpeed;
    public int OpenedDorCount;
    public int UnRepairedFieldCount;
    public string RepairItemsTag;
    public string DoorItemTag;
    private void Start()
    {
        
    }
    float carpan = 0F;
    private void Update()
    {

        GameObject[] RepairCount =  GameObject.FindGameObjectsWithTag(RepairItemsTag);
        DoorStatus[] DoorCount = GameObject.FindObjectsOfType<DoorStatus>();
        
        // opened DoorCount
        UnRepairedFieldCount = RepairCount.Length;
        var ds = DoorCount.Where(x => x.IsDoorOpened == true).ToList();
        OpenedDorCount = ds.Count;
        carpan = OpenedDorCount + UnRepairedFieldCount;
        waterObject.transform.Translate(Vector3.up*Time.deltaTime*floatSpeed*carpan);
    }





}
