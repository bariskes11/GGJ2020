using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuSystem : MonoBehaviour
{
    GameObject credits_obj;
    Button bckBtn;
    // Start is called before the first frame update
    private void Awake()
    {
        credits_obj = GameObject.Find("Credits");
    }
    void Start()
    {
        Button strt = GameObject.Find("BtnStartGame").GetComponent<Button>();

        bckBtn=   GameObject.Find("BackButton").GetComponent<Button>();
        
        strt.onClick.AddListener(() => StartGame());
        Button btnCredits = GameObject.Find("BtnCredits").GetComponent<Button>();
        btnCredits.onClick.AddListener(() => showCredits());
        credits_obj.SetActive(false);
        bckBtn.onClick.AddListener(() => HideCredits());
        bckBtn.enabled = false;

    }
    void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    void showCredits()
    {

        credits_obj.SetActive(true);
        bckBtn.enabled = true;

    }
    void HideCredits()
    {
        credits_obj.SetActive(false);
        bckBtn.enabled = false;

    }

}
