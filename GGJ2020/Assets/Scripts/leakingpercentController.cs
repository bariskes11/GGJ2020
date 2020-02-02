using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

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

    public float CurrentPercent;
    private RigidbodyFirstPersonController currentController;

    float BegginingSpeed=4f;
    private void Awake()
    {
        currentController = GameObject.FindObjectOfType<RigidbodyFirstPersonController>();
        startupDistance = Vector3.Distance(waterSystem.transform.position, gameOverSystem.transform.position);
        for (int i = 0; i < RoomCount; i++)
        {
            HUDRooms[i].SetActive(false);
        }

        for (int d = 0; d < DoorCount; d++)
        {
            HUDDoors[d].SetActive(false);
        }
        for (int e = 0; e < EngineCount; e++)
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
            catch (Exception ex)
            {
            }
        }

        // Engine Durumu ile ilgil ikısımda alert








        // 1. odada kaç tane arıza var.....
        for (int i = 0; i < RoomCount; i++)
        {
            string roomName = "room" + (i + 1).ToString();
            string RoomHUDname = "Room" + (i + 1).ToString();
            GameObject gmm = GameObject.FindGameObjectWithTag(roomName);
            if (gmm == null)
                continue;
            FixingItem[] repairItemsInroom = gmm.GetComponentsInChildren<FixingItem>();
            if (repairItemsInroom != null)
            {
                //Debug.Log("oda Adı=" + roomName + "  odadakiArizaar  " + repairItemsInroom.Length.ToString());

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
        CurrentPercent = 100 - ((distance / startupDistance) * 100);
        float carpan = Mathf.Clamp(CurrentPercent, 1, 99);
        Debug.Log(carpan);
        CurrentPercent = Mathf.Clamp(CurrentPercent, 0F, 100F);
        percentText.text = Convert.ToInt32(CurrentPercent).ToString();

        currentController.movementSettings.ForwardSpeed = BegginingSpeed -  ((CurrentPercent/100F)*BegginingSpeed);
        string EngineName = "Engine";
        WaterPulpSystem[] wtr = GameObject.FindObjectsOfType<WaterPulpSystem>();
        if (wtr != null)
        {
            int calisanSayisi = wtr.Length;
            int ArizaliSayisi = 2 - calisanSayisi;
            for (int i = 0; i < ArizaliSayisi; i++)
            {
                HUDEngines[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                HUDEngines[i].SetActive(true);
            }

        }



        /// Debug.Log(percent);

    }

    // Update is called once per frame

}
