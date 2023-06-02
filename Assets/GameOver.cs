using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject CongratsPanel;



    private void Start()
    {
        gameOverPanel.SetActive(false);
        CongratsPanel.SetActive(false);
        
    }


    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void ShowCongrats()
    {
 
      CongratsPanel.SetActive(true) ;
        
    }
}
