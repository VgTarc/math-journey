using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Coin : MonoBehaviour , IDataPersistence
{
    [SerializeField] private string coinID;
    [SerializeField] public bool collected = false;


   
    public void Collect()
    {
        if (collected) return;
        else
        {
            collected = true;
            gameObject.SetActive(false);
        }
            
    }




    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        coinID = System.Guid.NewGuid().ToString();
    }

    public void LoadData(GameData data)
    {
        data.coinsCollected.TryGetValue(coinID, out collected);
        if (collected)
        {
            gameObject.SetActive(false);
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.coinsCollected.ContainsKey(coinID))
        {
            data.coinsCollected.Remove(coinID);
        }
        data.coinsCollected.Add(coinID,collected);
    }





}
