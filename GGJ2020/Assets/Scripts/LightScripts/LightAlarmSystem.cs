using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAlarmSystem : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject AlarmLights;
    
    public string RoomName;
    void Start()
    {
       GameObject g=  Instantiate(AlarmLights, transform);
        AlarmLights = g;
        g.SetActive(false);
    }
    private void Update()
    {
        GameObject rm = GameObject.Find(RoomName);
        if (rm!=null)
        {
            AlarmLights.SetActive(true);
        }
        else
        {
            AlarmLights.SetActive(false);
        }
    }
    // Update is called once per frame

}
