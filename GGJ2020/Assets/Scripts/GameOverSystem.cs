using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverSystem : MonoBehaviour
{

    [SerializeField]
    public GameObject gameoverPanel;
    [SerializeField]
    public GameObject restartButton;
    

    private void Awake()
    {
        
        Button btn= restartButton.GetComponent<Button>();
        btn.onClick.AddListener(() => RestartGame());
        gameoverPanel.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collider Çarptı" + collision.gameObject.tag);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Çarptı" + other.tag);
        gameoverPanel.SetActive(true);
      Time.timeScale = 0F;
    }
  public  void RestartGame()
    {
        int SceneIndex=SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(SceneIndex);
    }

}
