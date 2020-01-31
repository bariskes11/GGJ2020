using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorStatus : MonoBehaviour
{
    public bool IsDoorOpened;
    private AudioSource doorSounds;
    public AudioClip opendoor;
    public AudioClip closedoor;
    private void Start()
    {
        doorSounds = this.GetComponent<AudioSource>();

    }
    public void SetDoorStatus()
    {
        /// kapi açiksa kapi açik animasyonu oynatilacak
        IsDoorOpened = !IsDoorOpened;
        if (IsDoorOpened)
        {
            doorSounds.PlayOneShot(opendoor);
        }
        else
        {
            doorSounds.PlayOneShot(closedoor);
        }
    
    }
    
}

