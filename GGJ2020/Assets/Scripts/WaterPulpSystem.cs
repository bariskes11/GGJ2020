using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterPulpSystem : MonoBehaviour
{
    public float StartingRotation;
    public float MaxPressure;
    public float CurrentPressure;
    public bool StartedToWork = false;
    public float WorkTimeout = 15F;
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
        if (StartedToWork)
        {
         //   Indicator.
        }
    }

    public void StartWorking()
    {
       var rslt=  Quaternion.Euler(new Vector3(260F, 0F, 0F));
        Indicator.transform.rotation = rslt;
        StartedToWork = true;
        StartCoroutine(StartToRotatePressure());
    }
    IEnumerator StartToRotatePressure()
    {
        yield return new WaitForSeconds(WorkTimeout);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            InteractiveText.text = SetText;

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
