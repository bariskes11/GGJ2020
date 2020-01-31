using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tools
{
    public int ItemCount { get; set; }
    public int MyProperty { get; set; }
}

public class PlayerController : MonoBehaviour
{
    public float raycashLenght;
    public Color[] colors= new Color[] { Color.blue, Color.red, Color.green};

    public int ToolCount = 0;
    public int MaxItemCount = 3;
    public Image crosshairImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    LineRenderer line = new LineRenderer();
   

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 ray = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        //line = new LineRenderer();
       // line.SetPosition(0, transform.position);

        RaycastHit hit;

        if (Physics.Raycast(ray, Camera.main.transform.forward, out hit, raycashLenght))
        {
            if (hit.collider != null && hit.collider.tag == "Item")
            {
                crosshairImage.color = colors[2];
                if (Input.GetKey(KeyCode.Mouse0))
                {

                    // item alınabilir
                    this.
                }
            }
            else
            {
                crosshairImage.color = colors[0];
            }
        }
        else
        {
            crosshairImage.color = colors[1];
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {

            /// item var ise  son Itemdan ateşşşş
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            // bir sonraki Item
        }
    }
}
