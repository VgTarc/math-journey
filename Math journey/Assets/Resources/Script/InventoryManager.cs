using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public GameObject inventoryMenu;
    private bool menuActivated;
    public ItemSlot[] itemSlot;


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
