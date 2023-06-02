using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SocreText : MonoBehaviour
{
    public static int coinCounter;
    public TextMeshProUGUI scoreText;
    [SerializeField] private GameOver congratsObject;
    [SerializeField] private PlayerController playerController;
    public bool congratsText = false;

    void Start()
    {
        coinCounter = 0;
        scoreText.text = "Score" + coinCounter;

    }

    // Update is called once per frame
    void Update()
    {
        if(coinCounter == 200)
        {
            congratsText=true;
            congratsObject.ShowCongrats();
            playerController.GetComponent<Rigidbody>().velocity = Vector3.zero;
            playerController.GetComponent<PlayerController>().enabled = false;
        }
        //Debug.Log(coinCounter.counter);
        scoreText.text= "Score: " + coinCounter.ToString();

    }
}