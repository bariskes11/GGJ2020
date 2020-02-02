using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

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
    public GameObject Explosion;
    private AudioSource audi;


    public string SetText = "Press E to Start Engine";

    private void Awake()
    {
        audi = GetComponent<AudioSource>();
        InteractiveText = GameObject.Find("PressEText").GetComponent<Text>();
    }




    // Update is called once per frame
    void Update()
    {

        if (StartedToWork)
        {
            Indicator.transform.Rotate(-Time.deltaTime * 20F, 0, 0, Space.World);
        }
    }


    public void Explode()
    {

        Instantiate(Explosion, this.transform.position,Quaternion.identity);
        StartCoroutine(wait());
        HeadBob ply = GameObject.FindObjectOfType<HeadBob>();
        ply.ShakeCamera(15,1.5F);
        GameObject.Destroy(gameObject);
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(1F);
    }

    public void StartWorking()
    {
        audi.Play();
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
            PlayerController plyr = other.transform.root.GetComponentInChildren<PlayerController>();
            plyr.SetCurrentWaterEngine(this.gameObject);
            /// press E to Start
        }
        else
        {
            InteractiveText.text = "";
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
