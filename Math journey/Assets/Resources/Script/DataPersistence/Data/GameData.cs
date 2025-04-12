using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{

    public Vector3 playerPositon; // store player positon
    public SerializableDictionary<string, bool> coinsCollected;


    public GameData()
    {
        playerPositon = Vector3.zero;
        coinsCollected = new SerializableDictionary<string, bool>();
    }
}
