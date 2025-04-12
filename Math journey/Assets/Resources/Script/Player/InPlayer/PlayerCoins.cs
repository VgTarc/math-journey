using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCoins : MonoBehaviour , IDataPersistence
{

    public Coins coinScript;
    private TMP_Text coinText;
    

    [Header("Coins")]

    public int Coin = 0;


    public void LoadData(GameData data)
    {
        foreach (KeyValuePair<string, bool> pair in data.coinsCollected)
        {
            if(pair.Value)
            {
                Coin++;
            }
        }
    }


    public void SaveData(ref GameData data) 
    { 
        // no data needs to be saved for this script
    }







    private void Start()
    {
        coinText = GameObject.Find("CoinCount").GetComponent<TMP_Text>();
    }

    private void Update()
    {
        coinText.text = Coin.ToString();
    }


  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Coin"))
        {
            Coin coin = collision.GetComponent<Coin>();
            if(coin != null && coin.collected == false)
            {
                coin.Collect();
                GetCoin(1);
            }
            
        }
    }

  

    public void GetCoin(int amount)
    {
        Coin += amount;
    }
}
