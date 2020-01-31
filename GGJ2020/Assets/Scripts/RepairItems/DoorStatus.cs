using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorStatus : MonoBehaviour
{
    public bool IsDoorOpened;

    public void SetDoorStatus()
    {
        /// kapi açiksa kapi açik animasyonu oynatilacak
        IsDoorOpened = !IsDoorOpened;
        Debug.Log(" Kapi Açık "+ IsDoorOpened);
    
    }
    
}

