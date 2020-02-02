using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuSystem : MonoBehaviour
{
    GameObject credits_obj;
    Button bckBtn;
    GameObject Instructions;
    // Start is called before the first frame update
    private void Awake()
    {
        credits_obj = GameObject.Find("Credits");
        Instructions = GameObject.Find("Instructions");
    }
    void Start()
    {
        Button strt = GameObject.Find("BtnStartInst").GetComponent<Button>();
        Button btnStartGame = GameObject.Find("BtnStartGame").GetComponent<Button>();
        btnStartGame.onClick.AddListener(() => StartGame());

        bckBtn =   GameObject.Find("BackButton").GetComponent<Button>();
        strt.onClick.AddListener(() => ShowInts());
        Button btnCredits = GameObject.Find("BtnCredits").GetComponent<Button>();
        
        btnCredits.onClick.AddListener(() => showCredits());
        bckBtn.onClick.AddListener(() => HideCredits());
        bckBtn.enabled = false;
        credits_obj.SetActive(false);
        Instructions.SetActive(false);
    }
    void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void ShowInts()
    {
        Instructions.SetActive(true);
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
