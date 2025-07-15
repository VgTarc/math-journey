using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour , IDataPersistence
{

    public GameObject inventoryMenu;
    public bool menuActivated;
    public ItemSlot[] itemSlot;

    public ItemSo[] itemSOs; // item SO array


    // Start is called before the first frame update
    //void Start()
    //{
    //    foreach (var itemSo in itemSOs)
    //    {
    //        Debug.Log("ItemSo: " + itemSo.itemName);
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && menuActivated)
        {
            inventoryMenu.SetActive(false); // Close the menu
            menuActivated = false;
        }

        else if (Input.GetKeyDown(KeyCode.E) && !menuActivated)
        {
            inventoryMenu.SetActive(true); // Open the menu
            menuActivated = true;
        }


        
    }


    public bool UseItem(string itemName)
    {
        for (int i = 0; i < itemSOs.Length; i++) // Loop through the array to find the same itemname
        {
            if (itemSOs[i].itemName == itemName)
            {
               bool usable = itemSOs[i].UseItem();
                return usable;
            }
            
        }
        return false;
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        // ✅ หา ItemSo ที่ตรงกับ itemName
        ItemSo matchedSo = null;
        foreach (var so in itemSOs)
        {
            if (so.itemName == itemName)
            {
                matchedSo = so;
                break;
            }
        }

        if (matchedSo == null)
        {
            Debug.LogError("ItemSO not found for item: " + itemName);
            return quantity;
        }

        // ✅ เติมช่องที่มีของอยู่ก่อน
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].itemName == itemName && itemSlot[i].quantity < itemSlot[i].maxNumberOfItems)
            {
                int leftover = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription, matchedSo);
                if (leftover > 0)
                    quantity = leftover;
                else
                    return 0;
            }
        }

        // ✅ เติมช่องว่าง
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (!itemSlot[i].isFull && string.IsNullOrEmpty(itemSlot[i].itemName))
            {
                int leftover = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription, matchedSo);
                if (leftover > 0)
                    quantity = leftover;
                else
                    return 0;
            }
        }

        return quantity;
    }









    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }

    //============== SAVE AND LOAD ======================//
    public void SaveData(ref GameData data)
    {
        data.inventoryItems.Clear();

        foreach(var slot in itemSlot)
        {
            if (slot == null || slot.quantity <= 0) continue;
            if (slot.itemSprite == null) continue;

            Debug.Log("Sprite name = " + slot.itemSprite.name);

            string spritePath = GetSpritePathByItemType(slot.itemName,slot.itemSprite.name);


                InventoryItemData itemData = new InventoryItemData
                {
                    itemName = slot.itemName,
                    quantity = slot.quantity,
                    itemDescription = slot.itemDescription,
                    spritePath = spritePath
                };

                data.inventoryItems.Add(itemData);
            
        }
    }

    public void LoadData(GameData data)
    {

        Debug.Log("Loading Inventory Data");

        foreach (var slot in itemSlot)
        {
            slot.EmptySlot(); // ถ้ามีฟังก์ชันนี้ใน ItemSlot ให้เรียกใช้เพื่อล้างข้อมูล
        }

        foreach (var item in data.inventoryItems)
        {
            Sprite loadedSprite = Resources.Load<Sprite>(item.spritePath);
           if(loadedSprite == null)
           {
                Debug.LogError("Sprite not found at path: " + item.spritePath);
           }
            AddItem(item.itemName, item.quantity, loadedSprite, item.itemDescription);
        }
    }

    public string GetSpritePathByItemType(string itemName, string spriteName)
    {
        foreach(var itemSo in itemSOs)
        {
            if(itemSo.itemName == itemName)
            {
                switch(itemSo.itemType)
                {
                    case ItemSo.ItemType.Potion:
                        return "InGameSprites/Potion/" + spriteName;
                    case ItemSo.ItemType.Book:
                        return "InGameSprites/Book/" + spriteName;
                    default:
                        return "InGameSprites/" + spriteName;
                }
            }
        }

        return "InGameSprites/" + spriteName;
    }



}
