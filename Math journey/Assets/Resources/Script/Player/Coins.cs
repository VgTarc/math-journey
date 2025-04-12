using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coins : MonoBehaviour , IDataPersistence
{
    [Header("Coins")]
    public int Coin; 

    [Header("PlayerScript")]
    private PlayerCoins playerCoins;

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

        playerCoins = playerObj.GetComponent<PlayerCoins>();
        if (playerCoins == null)
        {
            Debug.LogError("playerCoins component not found on Player GameObject.");
            return;
        }

        Coin = playerCoins.Coin;
        UpdateCoinUI();

        if (coinText == null)
        {
            Debug.LogError("CoinText is not assigned in the Inspector!", this);
        }
    }

    void Update()
    {
       
        if (Coin != playerCoins.Coin) 
        {
            Coin = playerCoins.Coin; 
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

    //========================== SAVE AND LOAD ==========================================//
    public void LoadData(GameData data)
    {

    }

    public void SaveData(ref GameData data)
    {

    }
    //========================== SAVE AND LOAD ==========================================//
}