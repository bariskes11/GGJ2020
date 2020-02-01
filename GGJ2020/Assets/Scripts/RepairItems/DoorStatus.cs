using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorStatus : MonoBehaviour
{
    public bool IsDoorOpened;
    private AudioSource doorSounds;
    public AudioClip opendoor;
    public AudioClip closedoor;
    public Animator anim;
    private string DoorStatusparamName = "DoorStatus";
    private void Start()
    {
        doorSounds = this.GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

    }
    public void SetDoorStatus()
    {
        // bu animasyonlar oynuyorsa işlem var demektir 
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            Debug.Log("Animasyon bitmedi.....");
            return;
        }
        
        /// kapi açiksa kapi açik animasyonu oynatilacak
        IsDoorOpened = !IsDoorOpened;
        if (IsDoorOpened)
        {
            doorSounds.PlayOneShot(opendoor);
            anim.SetInteger(DoorStatusparamName, 1);
        }
        else
        {
            doorSounds.PlayOneShot(closedoor);
            anim.SetInteger(DoorStatusparamName, -1);
        }
    
    }
    
}

