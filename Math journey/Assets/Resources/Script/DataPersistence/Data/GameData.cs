using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{

    public Vector3 playerPositon; // store player positon
    public SerializableDictionary<string, bool> coinsCollected; // specific coin adn if it collected
    public int playerCoins;
    public int stompDamage;
    public List<InventoryItemData> inventoryItems;
    public SerializableDictionary<string, bool> npcTalked;
    public int playerHealth;
    public SerializableDictionary<string, bool> playerTalked;
    public SerializableDictionary<string, bool> doorsDestroy;
    public SerializableDictionary<string, bool> csTrigger;
    public SerializableDictionary<string, bool> itemCollected;
    public List<WorldItemData> worldItems;
    public SerializableDictionary<string, bool> Monster;
    public SerializableDictionary<string, bool> scenesInitialized;
    public string currentSceneName;
    public SerializableDictionary<string, string> changedPictureSprites;
    public SerializableDictionary<string, bool> chestInteract;
    public bool initiate;

    // for checkpoint
    public float x;
    public float y;
    public float z;
    //--------



    public GameData()
    {
        playerPositon = new Vector3(-7,0,0);
        coinsCollected = new SerializableDictionary<string, bool>();
        playerCoins = 0;
        stompDamage = 5;
        inventoryItems = new List<InventoryItemData>();
        npcTalked = new SerializableDictionary<string, bool>();
        playerTalked = new SerializableDictionary<string, bool>();
        playerHealth = 100;
        doorsDestroy = new SerializableDictionary<string, bool>();
        csTrigger = new SerializableDictionary<string, bool>();
        itemCollected = new SerializableDictionary<string, bool>();
        worldItems = new List<WorldItemData>();
        Monster = new SerializableDictionary<string, bool>();
        
        currentSceneName = "";
        changedPictureSprites = new SerializableDictionary<string, string>();
        chestInteract = new SerializableDictionary<string, bool>();
        x = 0f;
        y = 0f;
        z = 0f;
        



    }
}

[System.Serializable]
public class InventoryItemData
{
    public string itemName;
    public int quantity;
    public string itemDescription;
    public string spritePath;
}

[System.Serializable]
public class WorldItemData
{
    public string itemID;
    public int quantity;
    public float[] position;
}
