using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRepairItems: MonoBehaviour
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
            if (NextSpanTime == 0F)
                NextSpanTime = 1F;
            float ItemY = Random.Range(minVTR.y, maxVTR.y);
            float Itemx = Random.Range(minVTR.x, maxVTR.x);
            Vector3 position = new Vector3(Itemx, ItemY, minVTR.z);
            Instantiate(brokenObject, position, Quaternion.identity);
        }
    }

}
