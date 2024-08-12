using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public GameObject inventoryMenu;
    private bool menuActivated;
    public ItemSlot[] itemSlot;

    public ItemSo[] itemSOs; // item SO array


    // Start is called before the first frame update
    void Start()
    {
        
    }

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
        for (int i = 0; i < itemSlot.Length; i++ )
        {
            if (itemSlot[i].isFull == false && itemSlot[i].itemName == name || itemSlot[i].quantity == 0)
            {
                int leftOverItem = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                if(leftOverItem > 0)
                {

                    leftOverItem = AddItem(itemName, leftOverItem, itemSprite, itemDescription);
                    
                }
                return leftOverItem;

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
}
