using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CointConroll : MonoBehaviour
{
    
    [SerializeField] PlayerController game_player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SocreText.coinCounter += 1;
            Debug.Log("carpti");
            Destroy(gameObject);
        }
    }
}
