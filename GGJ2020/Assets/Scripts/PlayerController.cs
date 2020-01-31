using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventory
{
    public int ItemCount { get; set; }
    public int RepairType { get; set; }
}

public class PlayerController : MonoBehaviour
{
    public float raycashLenght;
    public Color[] colors= new Color[] { Color.blue, Color.red, Color.green};
    public List<ItemInventory> Inventory = new List<ItemInventory>();

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
               RepairTool rt=  hit.collider.transform.root.gameObject.GetComponent<RepairTool>();
                if (Input.GetKeyDown(KeyCode.Mouse0) && this.ToolCount<MaxItemCount)
                {
                    //
                    // item alınabilir
                    this.ToolCount++;
                    Inventory.Add(new ItemInventory
                    {
                        ItemCount = ToolCount,
                        RepairType = rt.RepairType
                    });
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
        Debug.Log(Inventory);
    }
}
