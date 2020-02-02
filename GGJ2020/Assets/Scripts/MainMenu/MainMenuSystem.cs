using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       Button strt=  GameObject.Find("BtnStartGame").GetComponent<Button>();
        strt.onClick.AddListener(() => StartGame());
        Button  btnCredits= GameObject.Find("BtnCredits").GetComponent<Button>();
        btnCredits.onClick.AddListener(() => showCredits());
    }
    void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    void showCredits()
    {
       Animator anim= GameObject.Find("CreditsSlide").GetComponent<Animator>();
        anim.SetInteger("show", 1);

    }

}
