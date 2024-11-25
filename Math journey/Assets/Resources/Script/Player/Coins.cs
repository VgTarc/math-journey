using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [Header("Coins")]
    public int Coin; 

    [Header("PlayerScript")]
    private PlayerController playerController;

    [Header("CoinText")]
    public TextMeshProUGUI coinText;

    void Start()
    {
        GameObject playerObj = GameObject.Find("Player");
        if (playerObj == null)
        {
            Debug.LogError("Player GameObject not found.");
            return;
        }

        playerController = playerObj.GetComponent<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController component not found on Player GameObject.");
            return;
        }

        Coin = playerController.Coin;
        UpdateCoinUI();

        if (coinText == null)
        {
            Debug.LogError("CoinText is not assigned in the Inspector!", this);
        }
    }

    void Update()
    {
       
        if (Coin != playerController.Coin) 
        {
            Coin = playerController.Coin; 
            UpdateCoinUI(); 
        }
    }

    public void UpdateCoinUI()
    {
        coinText.text = Coin.ToString();
    }

  
    public void UpdateCoinCount(int newCoinValue)
    {
        Coin = newCoinValue; 
        UpdateCoinUI(); 
    }
}