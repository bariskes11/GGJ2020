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



    private void Awake()
    {

        startupDistance = Vector3.Distance(waterSystem.transform.position, gameOverSystem.transform.position);
        for (int i = 1; i < RoomCount; i++)
        {
            HUDRooms[i].SetActive(false);
        }
    }

    private void Update()
    {

        // 1. odada kaç tane arıza var.....
        for (int i = 1; i < RoomCount; i++)
        {
            string roomName = "room" + i.ToString();
            string RoomHUDname = "Room" + i.ToString();
            GameObject gmm = GameObject.FindGameObjectWithTag(roomName);
            FixingItem[] repairItemsInroom = gmm.GetComponentsInChildren<FixingItem>();
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



        float distance = Vector3.Distance(waterSystem.transform.position, gameOverSystem.transform.position);
        float percent = 100 - ((distance / startupDistance) * 100);
        percentText.text = Convert.ToInt32(percent).ToString();
        /// Debug.Log(percent);

    }

    // Update is called once per frame

}
