using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static int totalCoins = 0;

    void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

  void OnTriggerEnter2D(Collider2D kolikko)
    {
        
        if (kolikko.CompareTag("Player"))
        {
            totalCoins++;
            Debug.Log("You currently have " + Coin.totalCoins + " Coins.");
            Destroy(gameObject);
        }
    }
}
