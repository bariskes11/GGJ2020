using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRepairItems : MonoBehaviour
{
    public float maxX;
    public float maxY;
    private BoxCollider bCollider;
    public GameObject brokenObject;
    public Vector3 maxVTR;
    public Vector3 minVTR;


    
    public float minTime;
    public float maxtime;
    public float NextSpanTime;
    public float wallRotation;

    private void Awake()
    {
        bCollider = this.GetComponent<BoxCollider>();
        maxVTR = bCollider.bounds.max;
        minVTR = bCollider.bounds.min;
        //print(maxVTR);
        //print(minVTR);
        StartCoroutine(StartAlgorithm());
    }
    IEnumerator StartAlgorithm()
    {
        while (true)
        {
            yield return new WaitForSeconds(NextSpanTime);
            NextSpanTime = Random.Range(minTime, maxtime);
            float ItemY = Random.Range(0, 1F); // tüm komppantumanlar 0 ile 1 arası
            StartCoroutine(TimeOutForWait());

            float test_z = Random.Range(minVTR.z, maxVTR.z);
            //float Itemx = Random.Range(minTransform.position.z, maxTrasnform.position.z);
            Debug.Log(   " iTEM x" + test_z + "  Item Y"+ ItemY );
            Vector3 position = new Vector3(0, ItemY, test_z);
            GameObject gm = Instantiate(brokenObject, position, Quaternion.identity);
            gm.transform.SetParent(transform);
            Vector3 rt = new Vector3(0, wallRotation, 0);
            gm.transform.Rotate(rt);
            gm.transform.localPosition = new Vector3(0,ItemY, test_z);
        }
    }



    IEnumerator TimeOutForWait()
    {
        yield return new WaitForSeconds(0.5F);

    }
}
