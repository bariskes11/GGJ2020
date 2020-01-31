using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// item tipi ve item ın sayisi tutulur.
/// </summary>
public class ItemInventory
{
    public int ItemCount { get; set; }
    public int RepairType { get; set; }
    public bool IsSelected { get; set; }
}

public class PlayerController : MonoBehaviour
{
    public float raycashLenght;
    public Text Item1Count;
    public GameObject torchFire;
    private ParticleSystem torchParticles;
    public Color[] colors= new Color[] { Color.blue, Color.red, Color.green};
    public List<ItemInventory> Inventory = new List<ItemInventory>();
    public Text HUD;
    public int ToolCount = 0;
    public int MaxItemCount = 3;
    public Image crosshairImage;

    public float BulletTimeOut;

    public float CurrentRepairIndex=0F;

    private void Awake()
    {
        torchParticles= torchFire.GetComponentInChildren<ParticleSystem>();
        torchParticles.gameObject.SetActive(false);
        Item1Count.text = "0";
    }

    public float HUDClearTimeOut=2F;

    IEnumerator  ClearHudText()
    {
        yield return new WaitForSeconds(HUDClearTimeOut);
        HUD.text = "";
    }

     

    // Start is called before the first frame update
    void Start()
    {
        
    }
    LineRenderer line = new LineRenderer();


    void TakeRepairTool(RaycastHit hit)
    {
        crosshairImage.color = colors[2];
        RepairTool rt = hit.collider.transform.root.gameObject.GetComponent<RepairTool>();
        if (Input.GetKeyDown(KeyCode.Mouse0) && this.ToolCount < MaxItemCount)
        {
            //
            // item alınabilir
            this.ToolCount++;
            HUD.text += "Taken One Bullet \n\r";
            if (rt.RepairType == 0)
            {
                Item1Count.text = this.ToolCount.ToString();
                var inv = Inventory.Where(x => x.RepairType == 0).FirstOrDefault();
                if (inv == null)
                {
                    Inventory.Add(new ItemInventory
                    {
                        ItemCount = ToolCount,
                        RepairType = rt.RepairType,
                        IsSelected = true
                    });
                }
                else
                {
                    inv.ItemCount+=1;
                }
            }
            StartCoroutine(ClearHudText());
        }
        // max kısımı geçmeye çalışyıorsun
        if (Input.GetKeyDown(KeyCode.Mouse0) && this.ToolCount == MaxItemCount)
        {
            HUD.text += "!!Maximum Bullets Taken. \n\r";
            StartCoroutine(ClearHudText());
        }
    }
    float TimePassed = 0F;
    void UseBullet()
    {
        // bir mermi için 1 sn ateş etmek gerekir.
        var r = Inventory.Where(x => x.IsSelected == true).FirstOrDefault();
        TimePassed++;
        if (TimePassed> BulletTimeOut)
        {
            this.ToolCount--;
            r.ItemCount -= 1;
            Debug.Log(r.ItemCount);
            TimePassed = 0;
            Item1Count.text = r.ItemCount.ToString();
        }
    }

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
                TakeRepairTool(hit);
            }
            /// tamir etmee işlemi için bir click yaparken
            /// bullet bitmeden kapatmak gerekiyor 
            /// 
            var inverts = Inventory.Where(x => x.IsSelected == true).FirstOrDefault();
            

            if (inverts !=null && inverts.ItemCount>0 && hit.collider != null && hit.collider.tag.Contains("Repair") && Input.GetKey(KeyCode.Mouse0) )
            {

                UseBullet();
                torchParticles.gameObject.SetActive(true);
            }
            else
            {
                crosshairImage.color = colors[0];
                torchParticles.gameObject.SetActive(false);
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
