using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour , IDataPersistence
{
    [SerializeField] // make the variables see in the inspector
    public string itemName;

    [SerializeField]
    public int quantity;

    [SerializeField]
    public Sprite sprite;

    [TextArea]
    [SerializeField]
    public string itemDescription;

    private InventoryManager inventoryManager;

    public string itemID;
    public bool hasCollected = false;


    [ContextMenu("generate item GUID")]
    private void GenerateGuid()
    {
        itemID = System.Guid.NewGuid().ToString();
    }


    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            int leftOverItems = inventoryManager.AddItem(itemName, quantity, sprite, itemDescription);
            if (leftOverItems <= 0)
            {
                
                hasCollected = true;
                gameObject.SetActive(false);
            }
            else
            {
                quantity = leftOverItems;
            }
            
        }
    }

    public void LoadData(GameData data)
    {
        if (this == null || gameObject == null)
            return;

        data.itemCollected.TryGetValue(itemID, out hasCollected);
        if (hasCollected)
        {
            gameObject.SetActive(false);
            return;
        }

        var match = data.worldItems.Find(item => item.itemID == itemID);
        if (match != null)
        {
            quantity = match.quantity;
            transform.position = new Vector3(match.position[0], match.position[1], match.position[2]);
        }


    }


    public void SaveData(ref GameData data)
    {
        if (this == null || gameObject == null)
            return;


        WorldItemData itemData = new WorldItemData
        {
            itemID = itemID,
            quantity = quantity,
            position = new float[] { transform.position.x, transform.position.y, transform.position.z },
        };

        if (data.itemCollected.ContainsKey(itemID))
        {
            data.itemCollected.Remove(itemID);
        }
        data.itemCollected.Add(itemID, hasCollected);

        data.worldItems.Add(itemData);
    }



}
