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
    private GameObject torchParticles;
    public Text score_Text;
    public Color[] colors = new Color[] { Color.blue, Color.red, Color.green };
    public List<ItemInventory> Inventory = new List<ItemInventory>();
    public Text HUD;
    public int ToolCount = 0;
    public int MaxItemCount = 3;
    public Image crosshairImage;
    public float ScorePerFix = 100F;
    public float BulletTimeOut;
    [Tooltip("Oyuncu Kaç Sn Fix işlemi yapmalıdır")]
    public float FixtimeTimeOut;
    public float Score = 0F;
    public float CurrentRepairIndex = 0F;


    private List<string> playerTags = new List<string> { "Item", "Repair1", "Door" };

    private void Awake()
    {
        torchParticles = GameObject.Find("FireItem");
        torchParticles.gameObject.SetActive(false);
        Item1Count.text = "0";
    }

    void Start()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Door");
        foreach (GameObject item in items)
        {
            Debug.Log("Door taglı Objeler" + item.name.ToString());
        }
    }

    public float HUDClearTimeOut = 2F;

    IEnumerator ClearHudText()
    {
        yield return new WaitForSeconds(HUDClearTimeOut);
        HUD.text = "";
    }



    // Start is called before the first frame update



    void RepairCurrentObject(GameObject gmj)
    {
        // bir mermi için 1 sn ateş etmek gerekir.
        var r = Inventory.Where(x => x.IsSelected == true).FirstOrDefault();
        FixTimePassed++;
        if (FixTimePassed > FixtimeTimeOut)
        {

            GameObject.Destroy(gmj);
            Score += ScorePerFix;
            FixTimePassed = 0;
            score_Text.text = Score.ToString();
        }
    }

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
            rt.TakenSound();
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
                    inv.ItemCount += 1;
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
    float BulletTimePassed = 0F;
    float FixTimePassed = 0F;
    void UseBullet()
    {
        // bir mermi için 1 sn ateş etmek gerekir.
        var r = Inventory.Where(x => x.IsSelected == true).FirstOrDefault();
        BulletTimePassed++;
        if (BulletTimePassed > BulletTimeOut)
        {
            this.ToolCount--;
            r.ItemCount -= 1;
            // Debug.Log(r.ItemCount);
            BulletTimePassed = 0;
            Item1Count.text = r.ItemCount.ToString();
        }
    }
    void SetDoorStatus(GameObject gm)
    {
        DoorStatus dr = gm.GetComponent<DoorStatus>();
        dr.SetDoorStatus();
    }

    void SetColorOfCursor(string Tag)
    {
        switch (Tag)
        {
            case "Item":
                crosshairImage.color = colors[2];
                break;
            case "Repair1":
                crosshairImage.color = colors[0];
                break;

            case "Door":
                crosshairImage.color = colors[1];
                break;
            default:
                crosshairImage.color = colors[3];
                break;
        }
    }



    // Update is called once per frame
    bool makingDoorProcess = false;
    void FixedUpdate()
    {
        Vector3 ray = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));

        bool repairing = false;
        bool collided = false;
        
        WorkWaterEngine();

        RaycastHit hit;
        /// bir item a çarptik
        if (Physics.Raycast(ray, Camera.main.transform.forward, out hit, raycashLenght))
        {
            collided = true;
            SetColorOfCursor(hit.collider.tag);
            Debug.Log("Carptuğum Item ++" + hit.collider.tag);

            if (hit.collider != null && hit.collider.tag == "Item")
            {
                TakeRepairTool(hit);
                torchParticles.gameObject.SetActive(false);

            }
            /// tamir etmee işlemi için bir click yaparken
            /// bullet bitmeden kapatmak gerekiyor 
            /// 
            var inverts = Inventory.Where(x => x.IsSelected == true).FirstOrDefault();
            if (inverts != null && inverts.ItemCount > 0 && hit.collider != null && hit.collider.tag.Contains("Repair") && Input.GetKey(KeyCode.Mouse0))
            {
                crosshairImage.color = colors[0];
                UseBullet();
                torchParticles.gameObject.SetActive(true);
                RepairCurrentObject(hit.collider.gameObject);
                repairing = true;
                return;
            }
            if ( hit.collider != null && hit.collider.tag == "Door" && Input.GetKeyDown(KeyCode.Mouse0))
            {
                
                if (!makingDoorProcess)
                {
                    makingDoorProcess = true;
                    SetDoorStatus(hit.collider.gameObject);
                    StartCoroutine(doorStop());

                }

            }

        }
        if (!collided)
        {
            SetColorOfCursor("");
        }


        torchParticles.gameObject.SetActive(false);




    }
    IEnumerator doorStop()
    {
        yield return new WaitForSeconds(0.5F);
           makingDoorProcess = false;
    }

    public GameObject engine;
    public void SetCurrentWaterEngine(GameObject gmm)
    {
        engine = gmm;
    }

    void WorkWaterEngine()
    {
        string Txt = GameObject.Find("PressEText").GetComponent<Text>().text;
        if (Input.GetKeyDown(KeyCode.E) && Txt != "")
        {
            WaterPulpSystem wtr = engine.GetComponent<WaterPulpSystem>();
            if (!wtr.StartedToWork)
            {
                wtr.StartWorking();
            }
            else
            {
                wtr.Explode();
            }
        }
    }
}
