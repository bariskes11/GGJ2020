using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSystem : MonoBehaviour
{

    private AudioSource audio;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "musicsystem")
        {
           //Debug.Log("RRRR Şu an Buradayızz...." + this.gameObject.name);
           
            audio.volume = 0F;
            StartCoroutine(FadeInEffect());
            
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "musicsystem")
        {
         //   Debug.Log(" RRR Şu an BuradanAyrildik...." + this.gameObject.name);

            StartCoroutine(FadeOutEffect());
            
        }
    }
    IEnumerator FadeInEffect()
    {
        audio.Play();
        float v = audio.volume;
        while (v <= 1F)
        {
            yield return new WaitForSeconds(0.2F);
            v = v + 0.1F;
            audio.volume = v;
        }
    }



    IEnumerator FadeOutEffect()
    {
        float v = audio.volume;
        while (v > 0F)
        {
            yield return new WaitForSeconds(0.2F);
            v = v - 0.1F;
            audio.volume = v;
        }
        if (v == 0F)
        {
            audio.Stop();
        }
    }

}
