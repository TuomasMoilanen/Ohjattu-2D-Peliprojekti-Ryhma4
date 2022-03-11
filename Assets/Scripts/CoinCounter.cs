using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    Text CounterText;

    void Start()
    {
        CounterText = GetComponent<Text>();
    }

    
    void Update()
    {
        if(CounterText.text != Coin.totalCoins.ToString())
        {
            CounterText.text = Coin.totalCoins.ToString();
        }
    }
}
