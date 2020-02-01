using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterPulpSystem : MonoBehaviour
{
    public float StartingRotation=0F;
    public float MaxPressure;
    public float CurrentPressure;
    public bool StartedToWork = false;
    public float WorkTimeout = 15F;
    public float MaxRotation = 260F;
    public GameObject Indicator;
    public Text InteractiveText;

    public string SetText = "Press E to Start Engine";

    private void Awake()
    {

        InteractiveText = GameObject.Find("PressEText").GetComponent<Text>();
        
    }




    // Update is called once per frame
    void Update()
    {

        //Indicator.transform.Rotate(, 0, Space.World);


        if (StartedToWork)
        {
            //   Indicator.
            Indicator.transform.Rotate(-Time.deltaTime * 20F, 0, 0, Space.World);


        }
    }


    public void Explode()
    {
        GameObject.Destroy(gameObject);
    }


    public void StartWorking()
    {

        
        Indicator.transform.Rotate(MaxRotation, 0, 0, Space.World);
       
        StartCoroutine(StartToRotatePressure());
    }
    IEnumerator StartToRotatePressure()
    {
        StartedToWork = true;
      
        yield return new WaitForSeconds(WorkTimeout);
        StartedToWork = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            InteractiveText.text = SetText;
          PlayerController plyr=   other.transform.root.GetComponentInChildren<PlayerController>();
            plyr.SetCurrentWaterEngine(this.gameObject);
            /// press E to Start
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            /// press E to Start
            InteractiveText.text = "";
        }
    }
}
