using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class leakingpercentController : MonoBehaviour
{
    [SerializeField]
    GameObject waterSystem;
    [SerializeField]
    GameObject gameOverSystem;
    float startupDistance;
    public Text percentText;
    public int RoomCount = 3;
    public GameObject[] HUDRooms;
    public GameObject[] HUDDoors;
    public GameObject[] HUDEngines;

    public int DoorCount;
    public int EngineCount;



    private void Awake()
    {

        startupDistance = Vector3.Distance(waterSystem.transform.position, gameOverSystem.transform.position);
        for (int i = 0; i < RoomCount; i++)
        {
            HUDRooms[i].SetActive(false);
        }

        for (int d = 0; d < DoorCount; d++)
        {
            HUDDoors[d].SetActive(false);
        }
        for (int e  = 0; e < EngineCount; e++)
        {
            HUDEngines[e].SetActive(false);
        }
    }

    private void Update()
    {

        // acikKapi var ise ilgili Kısımdaalert
        for (int d = 0; d < DoorCount; d++)
        {
            try
            {
                string Doorname = "odaDoor" + (d + 1).ToString();
                string DoorHUDName = "Door" + (d + 1).ToString();
                DoorStatus dr = GameObject.Find(Doorname).GetComponent<DoorStatus>();
                if (dr == null)
                    continue;
                if (dr.IsDoorOpened)
                {
                    HUDDoors[d].SetActive(true);
                }
                else
                {
                    HUDDoors[d].SetActive(false);
                }
            }
            catch(Exception ex)
            {
            }
        }



        // 1. odada kaç tane arıza var.....
        for (int i = 0; i < RoomCount; i++)
        {
            string roomName = "room" + (i+1).ToString();
            string RoomHUDname = "Room" + (i+1).ToString();
            GameObject gmm = GameObject.FindGameObjectWithTag(roomName);
            if (gmm == null)
                continue;
            FixingItem[] repairItemsInroom = gmm.GetComponentsInChildren<FixingItem>();
            if (repairItemsInroom != null)
            {
                Debug.Log("oda Adı=" + roomName + "  odadakiArizaar  " + repairItemsInroom.Length.ToString());

                if (repairItemsInroom.Length > 0)
                {
                    HUDRooms[i].SetActive(true);
                }
                else
                {

                    HUDRooms[i].SetActive(false);
                }
            }


        }



        float distance = Vector3.Distance(waterSystem.transform.position, gameOverSystem.transform.position);
        float percent = 100 - ((distance / startupDistance) * 100);
        percentText.text = Convert.ToInt32(percent).ToString();
        /// Debug.Log(percent);

    }

    // Update is called once per frame

}
