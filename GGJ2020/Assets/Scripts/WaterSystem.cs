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
    public int StartedEngineCout;
    public string RepairItemsTag;
    public string DoorItemTag;
    public int EngineCarpan;
    public int Currentpercent;
    public int MinEngineStartPercent;
    
    private void Start()
    {
        
    }
    float carpan = 0F;
    private void Update()
    {

        GameObject[] RepairCount = GameObject.FindGameObjectsWithTag(RepairItemsTag);
        DoorStatus[] DoorCount = GameObject.FindObjectsOfType<DoorStatus>();
        WaterPulpSystem[] PulpCount = GameObject.FindObjectsOfType<WaterPulpSystem>();
        leakingpercentController crStatus = GameObject.FindObjectOfType<leakingpercentController>();
        int EngineCount = 0;
        if (PulpCount != null)
        {
            foreach (var item in PulpCount)
            {
                bool status = item.StartedToWork;
                if (status)
                    EngineCount++;
            }
        }  
        // opened DoorCount
        UnRepairedFieldCount = RepairCount.Length;
        var ds = DoorCount.Where(x => x.IsDoorOpened == true).ToList();
        OpenedDorCount = ds.Count;
        carpan = OpenedDorCount + UnRepairedFieldCount; 
        if (crStatus.CurrentPercent >= MinEngineStartPercent)
        {
            carpan-= (EngineCarpan * EngineCount);
        }

        Debug.Log(carpan + " cALİSAN mOTOR sAYİSİİİ  "+ EngineCount);
        
        
            waterObject.transform.Translate(Vector3.up * Time.deltaTime * floatSpeed * carpan);
        
    }





}
